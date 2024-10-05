using ApplicationMVC.ViewModels;
using Data.Contexts.Default;
using Data.Contracts;
using Data.Helper;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApplicationMVC.Controllers
{
    public class DevicesController(IUnitOfWork<DefaultDbContext> unitOfWork) : Controller
    {

        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5)
        {
            //var defaultDbContext = _context.Devices.Include(d => d.Category);
            var categories = await unitOfWork.Repository<Category>().GetAllAsync<GetCategoryForSelectionDTO>();
            var devices = await unitOfWork.Repository<Device>().GetAllAsync<GetDeviceDTO>(pageNumber, pageSize,order:d=>d.Id);
            var totalCount = await unitOfWork.Repository<Device>().GetCountAsync();
            var devicesPages = new Paging<GetDeviceDTO>(devices, totalCount, pageNumber, pageSize);
            ViewData["Categories"] = categories;
            return View(devicesPages);
        }
        [HttpPost]

        public async Task<IActionResult> Index(IFormCollection collection, int pageNumber = 1, int pageSize = 5)
        {


            var selectedCategoryId = int.Parse(collection["category"]!);
            string searchString = collection["searchString"].ToString();
            Expression<Func<Device, bool>> filter = q =>
            (string.IsNullOrEmpty(searchString) || q.Name.ToLower().Contains(searchString.ToLower())) &&
            (selectedCategoryId == 0 || q.CategoryId == selectedCategoryId);
            var devices = await unitOfWork.Repository<Device>().GetAllAsync<GetDeviceDTO>(pageNumber, pageSize, order: d => d.Id);
            var totalCount = await unitOfWork.Repository<Device>().GetCountAsync();
            var devicesPages = new Paging<GetDeviceDTO>(devices, totalCount, pageNumber, pageSize);

            var categories = await unitOfWork.Repository<Category>().GetAllAsync<GetCategoryForSelectionDTO>();
            ViewData["Categories"] = categories;
            return View(devicesPages);




        }

        // GET: Devices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await unitOfWork.Repository<Device>().GetAsync<GetDetailedDeviceDTO>(d => d.Id == id);

            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }
        [HttpGet("Devices/GetCategoryProperties")]
        public async Task<IActionResult> GetCategoryProperties([FromQuery] int categoryId)
        {
            var category = await unitOfWork.Repository<Category>().GetAsync<GetCategoryDTO>(c => c.Id == categoryId);
            if (category == null)
            {
                return NotFound();
            }

            return Json(category.Properties);
        }
        // GET: Devices/Create
        public async Task<IActionResult> Create()
        {
            var categories = await unitOfWork.Repository<Category>().GetAllAsync<GetCategoryDTO>();
            ViewData["Categories"] = categories;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostDeviceDTO deviceDTO, List<DevicePropertyViewModel> Properties)
        {
            if (ModelState.IsValid)
            {
                var device = new Device
                {
                    Name = deviceDTO.Name,
                    CategoryId = deviceDTO.CategoryId,
                    SerialNumber = deviceDTO.SerialNumber,
                    Memo = deviceDTO.Memo,
                    DeviceProperties = []
                };
                foreach (var prop in Properties)
                {
                    if (!string.IsNullOrEmpty(prop.Value))
                    {
                        device.DeviceProperties.Add(new DeviceProperty { PropertyId = prop.PropertyId, Value = prop.Value });
                    }
                }
                unitOfWork.Repository<Device>().Add(device);
                await unitOfWork.CompleteAsync();
                TempData["success"] = "Created successfully";
                return RedirectToAction(nameof(Index));
            }


            var categories = await unitOfWork.Repository<Category>().GetAllAsync<GetCategoryDTO>();
            ViewData["Categories"] = categories;
            return View(deviceDTO);
        }

        // GET: Devices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }

            var device = await unitOfWork.Repository<Device>().GetAsync<GetDetailedDeviceDTO>(d => d.Id == id, false, nameof(Device.DeviceProperties));
            if (device is null)
            {
                return NotFound();
            }
            var deviceCategoryAllProperties = await unitOfWork.Repository<Property>().GetAllAsync<GetPropertyDTO>(c => c.Categories.Any(c => c.Id == device.CategoryId));
            if (deviceCategoryAllProperties is null)
            {
                return NotFound();
            };
            if (device.Properties is null)
            {
                device.Properties = [];
            }
            var newDeviceProperties = deviceCategoryAllProperties.Select(dva => new GetDevicePropertiesDTO { PropertyId = dva.Id, Value = "", PropertyName = dva.Description }).Where(dp => !device.Properties.Any(d => d.PropertyId == dp.PropertyId));
            device.Properties.AddRange(newDeviceProperties);
            return View(device);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GetDetailedDeviceDTO deviceDTo)
        {
            if (id != deviceDTo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var device = await unitOfWork.Repository<Device>().GetAsync<Device>(d => d.Id == id, true, nameof(Device.DeviceProperties));
                //update old properties
                if (device != null)
                {
                    device.SerialNumber = deviceDTo.SerialNumber;
                    device.Memo = deviceDTo.Memo;
                    device.Name = deviceDTo.Name;

                    // device has no properties
                    if (device.DeviceProperties is null && deviceDTo.Properties?.Any() is true)
                    {
                        device.DeviceProperties = [];
                        device.DeviceProperties.AddRange(deviceDTo.Properties.Select(p => new DeviceProperty { PropertyId = p.PropertyId, Value = p.Value, DeviceId = device.Id }));
                    }
                    else if (deviceDTo.Properties?.Any() is true && device.DeviceProperties is not null)
                    {
                        var oldProperties = device.DeviceProperties;
                        foreach (var prop in deviceDTo.Properties)
                        {
                            var oldProp = oldProperties.FirstOrDefault(p => p.PropertyId == prop.PropertyId);
                            if (oldProp != null)
                            {
                                oldProp.Value = prop.Value;
                            }
                            else
                            {
                                device.DeviceProperties.Add(new DeviceProperty { PropertyId = prop.PropertyId, Value = prop.Value, DeviceId = device.Id });
                            }
                        }
                    }

                    try
                    {
                        unitOfWork.Repository<Device>().Modify(device);
                        await unitOfWork.CompleteAsync();
                        TempData["success"] = "updated successfully";
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!await DeviceExists(device.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(deviceDTo);
        }

        // GET: Devices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await unitOfWork.Repository<Device>().GetAsync<Device>(d => d.Id == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var device = await unitOfWork.Repository<Device>().GetAsync<Device>(d => d.Id == id);
            if (device != null)
            {
                unitOfWork.Repository<Device>().Remove(device);
            }

            await unitOfWork.CompleteAsync();
            TempData["success"] = "Deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> DeviceExists(int id)
        {
            return await unitOfWork.Repository<Device>().IsExistAsync(d => d.Id == id);
        }
    }

}
