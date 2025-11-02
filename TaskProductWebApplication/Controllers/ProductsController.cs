using Microsoft.AspNetCore.Mvc;
using TaskProductWebApplication.Data;
using TaskProductWebApplication.Models;

public class ProductsController : Controller
{
    private readonly ProductRepository _repository;

    public ProductsController(ProductRepository repository)
    {
        _repository = repository;
    }

    // GET: Products
    public async Task<IActionResult> Index(string searchTerm)
    {
        IEnumerable<Product> products;
        if (!string.IsNullOrWhiteSpace(searchTerm))
            products = await _repository.SearchAsync(searchTerm);
        else
            products = await _repository.GetAllAsync();

        ViewBag.SearchTerm = searchTerm;
        return View(products);
    }

    // GET: Products/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Products/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        if (ModelState.IsValid)
        {
            product.Created = DateTime.Now;
            await _repository.CreateAsync(product);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    // GET: Products/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null) return NotFound();
        return View(product);
    }

    // POST: Products/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            // Get the original product from DB
            var original = await _repository.GetByIdAsync(product.ProductID);
            if (original == null) return NotFound();

            // Preserve the original Created value
            product.Created = original.Created;

            await _repository.UpdateAsync(product);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    // GET: Products/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null) return NotFound();
        return View(product);
    }

    // POST: Products/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _repository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    // GET: Products/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var product = await _repository.GetByIdAsync(id);
        if (product == null) return NotFound();
        return PartialView("_ProductDetailsPartial", product);
    }
}