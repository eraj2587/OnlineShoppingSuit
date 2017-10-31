using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using ECommerce.Storage.Repository;
using log4net;
using NServiceBus;

namespace ECommerce.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILog _log;
        private readonly IRepository _repository;
        private readonly ISendOnlyBus _bus;

        public OrderController(IRepository repository, ILog log,ISendOnlyBus bus)
        {
            _repository = repository;
            _log = log;
            _bus = bus;
        }

        public async Task<ActionResult> Index()
        {
            _log.Info("Action Index has been fired.");
            var orders = _repository.All<Domain.Order>(x => x.CustomerDetail);
            return View(orders);
        }

        // GET: /Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Domain.Order order = _repository.Load<Domain.Order>(x => x.OrderId == id, x => x.CustomerDetail);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
          /* 
                    // GET: /Employee/Create
                    public ActionResult Create()
                    {
                        ViewBag.DepartmentID = new SelectList(_repository.All<Department>().ToList(), "ID", "DepartmentName");
                        return View();
                    }

                    // POST: /Employee/Create
                    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
                    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
                    [HttpPost]
                    [ValidateAntiForgeryToken]
                    public ActionResult Create([Bind(Include="ID,Name,Address,DepartmentID")] Employee employee)
                    {
                        if (ModelState.IsValid)
                        {
                            _repository.Add(employee);
                           // _repository.CommitChanges();
                            _bus.Send(new EmployeeAdded {Id = Guid.NewGuid(), EmployeeId = employee.ID});
                            return RedirectToAction("Index");
                        }

                        ViewBag.DepartmentID = new SelectList(_repository.All<Department>().ToList(), "ID", "DepartmentName", employee.DepartmentID);
                        return View(employee);
                    }

                    // GET: /Employee/Edit/5
                    public ActionResult Edit(int? id)
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        Employee employee = _repository.Load<Employee>(x => x.ID == id);
                        if (employee == null)
                        {
                            return HttpNotFound();
                        }
                        ViewBag.DepartmentID = new SelectList(_repository.All<Department>().ToList(), "ID", "DepartmentName", employee.DepartmentID);
                        return View(employee);
                    }

                    // POST: /Employee/Edit/5
                    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
                    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
                    [HttpPost]
                    [ValidateAntiForgeryToken]
                    public ActionResult Edit([Bind(Include="ID,Name,Address,DepartmentID")] Employee employee)
                    {
                        if (ModelState.IsValid)
                        {
                            _repository.Update(employee);
                            //_repository.CommitChanges();
                            return RedirectToAction("Index");
                        }
                        ViewBag.DepartmentID = new SelectList(_repository.All<Department>().ToList(), "ID", "DepartmentName", employee.DepartmentID);
                        return View(employee);
                    }

                    // GET: /Employee/Delete/5
                    public ActionResult Delete(int? id)
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        var employee = _repository.Load<Employee>(x => x.ID == id,x=>x.Department);
                        if (employee == null)
                        {
                            return HttpNotFound();
                        }
                        return View(employee);
                    }

                    // POST: /Employee/Delete/5
                    [HttpPost, ActionName("Delete")]
                    [ValidateAntiForgeryToken]
                    public ActionResult DeleteConfirmed(int id)
                    {
                        _repository.Delete<Employee>(id);
                        //_repository.CommitChanges();
                        return RedirectToAction("Index");
                    }
                    */
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
               
            }
            base.Dispose(disposing);
        }
    }
}
