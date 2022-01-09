// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using DailyDesktop.Core;
using DailyDesktop.Providers.Bing;
using DailyDesktop.Providers.CalvinAndHobbes;
using DailyDesktop.Providers.DeviantArt;
using DailyDesktop.Providers.FalseKnees;
using DailyDesktop.Providers.MTG;
using DailyDesktop.Providers.Pixiv;
using DailyDesktop.Providers.RedditEarthPorn;
using DailyDesktop.Providers.Unsplash;
using DailyDesktop.Providers.WikimediaCommons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DailyDesktop.Tests
{
    [TestClass]
    public class TestProviders
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestBing()
        {
            var provider = new BingProvider();
            WallpaperInfo wallpaper = provider.GetWallpaperInfo();

            TestContext.WriteLine("Image URI: " + wallpaper.ImageUri);
            TestContext.WriteLine("Author: " + wallpaper.Author);
            TestContext.WriteLine("Title: " + wallpaper.Title);
            TestContext.WriteLine("Title Uri: " + wallpaper.TitleUri);
            TestContext.WriteLine("Description: " + wallpaper.Description);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Author), "Null/whitespace author.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.TitleUri), "Null/whitespace title URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Description), "Null/whitespace description.");
        }

        [TestMethod]
        public void TestCalvinAndHobbes()
        {
            var provider = new CalvinAndHobbesProvider();
            WallpaperInfo wallpaper = provider.GetWallpaperInfo();

            TestContext.WriteLine("Image URI: " + wallpaper.ImageUri);
            TestContext.WriteLine("Title Uri: " + wallpaper.TitleUri);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.TitleUri), "Null/whitespace title URI.");
        }

        [TestMethod]
        public void TestDeviantArt()
        {
            var provider = new DeviantArtProvider();
            WallpaperInfo wallpaper = provider.GetWallpaperInfo();

            TestContext.WriteLine("Image URI: " + wallpaper.ImageUri);
            TestContext.WriteLine("Author: " + wallpaper.Author);
            TestContext.WriteLine("Author URI: " + wallpaper.AuthorUri);
            TestContext.WriteLine("Title: " + wallpaper.Title);
            TestContext.WriteLine("Title Uri: " + wallpaper.TitleUri);
            TestContext.WriteLine("Description: " + wallpaper.Description);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Author), "Null/whitespace author.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.AuthorUri), "Null/whitespace author URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.TitleUri), "Null/whitespace title URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Description), "Null/whitespace description.");
        }

        [TestMethod]
        public void TestFalseKnees()
        {
            var provider = new FalseKneesProvider();
            WallpaperInfo wallpaper = provider.GetWallpaperInfo();

            TestContext.WriteLine("Image URI: " + wallpaper.ImageUri);
            TestContext.WriteLine("Title: " + wallpaper.Title);
            TestContext.WriteLine("Title Uri: " + wallpaper.TitleUri);
            TestContext.WriteLine("Description: " + wallpaper.Description);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.TitleUri), "Null/whitespace title URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Description), "Null/whitespace description.");
        }

        [TestMethod]
        public void TestMTG()
        {
            var provider = new MTGProvider();
            WallpaperInfo wallpaper = provider.GetWallpaperInfo();

            TestContext.WriteLine("Image URI: " + wallpaper.ImageUri);
            TestContext.WriteLine("Author: " + wallpaper.Author);
            TestContext.WriteLine("Title: " + wallpaper.Title);
            TestContext.WriteLine("Title Uri: " + wallpaper.TitleUri);
            TestContext.WriteLine("Description: " + wallpaper.Description);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Author), "Null/whitespace author.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.TitleUri), "Null/whitespace title URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Description), "Null/whitespace description.");
        }

        [TestMethod]
        public void TestPixiv()
        {
            var provider = new PixivProvider();
            WallpaperInfo wallpaper = provider.GetWallpaperInfo();

            TestContext.WriteLine("Image URI: " + wallpaper.ImageUri);
            TestContext.WriteLine("Author: " + wallpaper.Author);
            TestContext.WriteLine("Author URI: " + wallpaper.AuthorUri);
            TestContext.WriteLine("Title: " + wallpaper.Title);
            TestContext.WriteLine("Title Uri: " + wallpaper.TitleUri);
            TestContext.WriteLine("Description: " + wallpaper.Description);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Author), "Null/whitespace author.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.AuthorUri), "Null/whitespace author URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.TitleUri), "Null/whitespace title URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Description), "Null/whitespace description.");
        }

        [TestMethod]
        public void TestRedditEarthPorn()
        {
            var provider = new RedditEarthPornProvider();
            WallpaperInfo wallpaper = provider.GetWallpaperInfo();

            TestContext.WriteLine("Image URI: " + wallpaper.ImageUri);
            TestContext.WriteLine("Author: " + wallpaper.Author);
            TestContext.WriteLine("Author URI: " + wallpaper.AuthorUri);
            TestContext.WriteLine("Title Uri: " + wallpaper.TitleUri);
            TestContext.WriteLine("Description: " + wallpaper.Description);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Author), "Null/whitespace author.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.AuthorUri), "Null/whitespace author URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.TitleUri), "Null/whitespace title URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Description), "Null/whitespace description.");
        }

        [TestMethod]
        public void TestUnsplash()
        {
            var provider = new UnsplashProvider();
            WallpaperInfo wallpaper = provider.GetWallpaperInfo();

            TestContext.WriteLine("Image URI: " + wallpaper.ImageUri);
            TestContext.WriteLine("Author: " + wallpaper.Author);
            TestContext.WriteLine("Author URI: " + wallpaper.AuthorUri);
            TestContext.WriteLine("Title: " + wallpaper.Title);
            TestContext.WriteLine("Title Uri: " + wallpaper.TitleUri);
            TestContext.WriteLine("Description: " + wallpaper.Description);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Author), "Null/whitespace author.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.AuthorUri), "Null/whitespace author URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.TitleUri), "Null/whitespace title URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Description), "Null/whitespace description.");
        }

        [TestMethod]
        public void TestWikimediaCommons()
        {
            var provider = new WikimediaCommonsProvider();
            WallpaperInfo wallpaper = provider.GetWallpaperInfo();

            TestContext.WriteLine("Image URI: " + wallpaper.ImageUri);
            TestContext.WriteLine("Author: " + wallpaper.Author);
            TestContext.WriteLine("Author URI: " + wallpaper.AuthorUri);
            TestContext.WriteLine("Title: " + wallpaper.Title);
            TestContext.WriteLine("Title Uri: " + wallpaper.TitleUri);
            TestContext.WriteLine("Description: " + wallpaper.Description);

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Author), "Null/whitespace author.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.AuthorUri), "Null/whitespace author URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.TitleUri), "Null/whitespace title URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Description), "Null/whitespace description.");
        }
    }
}
