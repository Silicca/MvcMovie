using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
		private static List<Movie> Db = new List<Movie>
		{
			new Movie{ ID = 0, Title = "Ghostbusters", Genre = "Comédie", ReleaseDate = new DateTime(1984,06,08), Price = 6.99M},
			new Movie{ ID = 1, Title = "Ghostbusters II", Genre = "Comédie", ReleaseDate = new DateTime(1989,06,16), Price = 6.99M},
			new Movie{ ID = 2, Title = "Mondiale des singes", Genre = "Action", ReleaseDate = new DateTime(1986,03,27), Price = 5.99M}
		};

		// GET: Movies
		public ActionResult Index(string movieGenre, string searchString)
		{
			var GenreLst = new List<string>();

			var GenreQry = from d in Db
						   orderby d.Genre
						   select d.Genre;

			GenreLst.AddRange(GenreQry.Distinct());
			ViewBag.movieGenre = new SelectList(GenreLst);

			var movies = from m in Db
						 select m;

			if (searchString!=null)
			{
				searchString = searchString.ToLower();
				movies = Db.Where(m => m.Title.ToLower().Contains(searchString));
			}
			if (!string.IsNullOrEmpty(movieGenre))
			{
				movies = movies.Where(m => m.Genre == movieGenre);
			}
			else
			{
				movies = Db;
			}
			return View(movies);
        }

		// GET: Movies/Details/5
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			Movie movie = Db.Where(m => m.ID == id).Select(m => m).ToList()[0];
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                Db.Add(movie);
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = Db.Where(m => m.ID == id).Select(m => m).ToList()[0];
			if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (ModelState.IsValid)
            {
				//db.Entry(movie).State = EntityState.Modified;
				//db.SaveChanges();
				Movie mo = Db.Where(m => m.ID == movie.ID).Select(m => m).ToList()[0];
				int index = Db.IndexOf(mo);
				Db[index] = movie;
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = Db.Where(m => m.ID == id).Select(m => m).ToList()[0];
			if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = Db.Where(m => m.ID == id).Select(m => m).ToList()[0];
			Db.Remove(movie);
            return RedirectToAction("Index");
        }

        /*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
