using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using firstApp.Models;

namespace firstApp.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext  _context;

        public PostController(ApplicationDbContext  context)
        {
            _context = context;
        }

        [HttpPost]

    [HttpPost]

    [HttpPost]
    public IActionResult Comment(int id, string comment)
    {
        var post = _context.Posts.Include(p => p.comments).FirstOrDefault(p => p.Id == id);
        if (post != null)
        {
            post.comments.Add(new Comment { comment_content = comment });
            _context.SaveChanges();
            return Json(new { comments = post.comments.Select(c => c.comment_content).ToList() });
        }
        return NotFound();
    }


        // GET: Posts
        public async Task<IActionResult> Index()
        {
            Console.WriteLine("In the post view home");
            return View(await _context.Posts.Include(p => p.comments).ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.comments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,Image")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Console.WriteLine("The id is {0}", id);
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.comments)
                .FirstOrDefaultAsync(m => m.Id == id);
            // var post = await _context.Posts.FindAsync(id);
            Console.WriteLine("the post is ");
            Console.WriteLine(post.comments);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,Image,Likes,Shares")] Post post, String newComment)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            // if (ModelState.IsValid)
            // {
                try
                {
                    // Retrieve the existing post including its comments
                    var existingPost = await _context.Posts.Include(p => p.comments).FirstOrDefaultAsync(p => p.Id == id);

                    if (existingPost != null)
                    {
                        existingPost.Content = post.Content;
                        existingPost.Image = post.Image;
                        existingPost.Likes = post.Likes;
                        existingPost.Shares = post.Shares;

                        // If there's a new comment, add it to the post's comments
                        if (!string.IsNullOrEmpty(newComment))
                        {
                            existingPost.comments.Add(new Comment { comment_content = newComment });
                        }

                        _context.Update(existingPost);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                // }
            }
            
                return RedirectToAction(nameof(Index));
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
