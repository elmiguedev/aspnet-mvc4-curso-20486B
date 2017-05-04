using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhotosMVC.Controllers;
using PhotosMVC.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PhotosMVCTest
{
    [TestClass]
    public class PhotoControllerTest
    {
        [TestMethod]
        public void Test_Index_Return_View()
        {
            PhotoController controller = new PhotoController(new FakePhotoSharingContext());
            var result = controller.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void Test_PhotoGallery_Model_Type()
        {
            PhotoController controller = new PhotoController(new FakePhotoSharingContext());
            var result = controller._PhotoGallery() as PartialViewResult;
            Assert.AreEqual(typeof(List<Photo>), result.Model.GetType());
        }

        [TestMethod]
        public void Test_GetImage_Return_Type()
        {
            PhotoController controller = new PhotoController(new FakePhotoSharingContext());
            var result = controller.GetImage(1) as ActionResult;
            Assert.AreEqual(typeof(FileContentResult), result.GetType());
        }
    }
}
