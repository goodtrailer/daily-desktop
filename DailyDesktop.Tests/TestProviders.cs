using DailyDesktop.Core;
using DailyDesktop.Core.Providers.Bing;
using DailyDesktop.Core.Providers.DeviantArt;
using DailyDesktop.Core.Providers.MTG;
using DailyDesktop.Core.Providers.Pixiv;
using DailyDesktop.Core.Providers.RedditEarthPorn;
using DailyDesktop.Core.Providers.Unsplash;
using DailyDesktop.Core.Providers.WikimediaCommons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DailyDesktop.Tests
{
    [TestClass]
    public class TestProviders
    {
        [TestMethod]
        public void TestBing()
        {
            var provider = new BingProvider();
            WallpaperInfo wallpaper = provider.GetWallpaperInfo();

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Author), "Null/whitespace author.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.TitleUri), "Null/whitespace title URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Description), "Null/whitespace description.");
        }

        [TestMethod]
        public void TestDeviantArt()
        {
            var provider = new DeviantArtProvider();
            WallpaperInfo wallpaper = provider.GetWallpaperInfo();

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Author), "Null/whitespace author.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.AuthorUri), "Null/whitespace author URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Title), "Null/whitespace title.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.TitleUri), "Null/whitespace title URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Description), "Null/whitespace description.");
        }

        [TestMethod]
        public void TestMTG()
        {
            var provider = new MTGProvider();
            WallpaperInfo wallpaper = provider.GetWallpaperInfo();

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

            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.ImageUri), "Null/whitespace image URI!");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Author), "Null/whitespace author.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.AuthorUri), "Null/whitespace author URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.TitleUri), "Null/whitespace title URI.");
            Assert.IsFalse(string.IsNullOrWhiteSpace(wallpaper.Description), "Null/whitespace description.");
        }
    }
}
