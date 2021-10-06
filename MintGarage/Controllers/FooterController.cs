﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MintGarage.Models.FooterContents.FooterSocialMedias;
using MintGarage.Models.FooterContents.FooterContactInfo;
using MintGarage.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace MintGarage.Controllers
{
    public class FooterController : Controller
    {

        private IFooterContactInfoRepository footerContactInfoRepository;
        private IFooterSocialMediaRepository footerSocialMediaRepository;
        private IWebHostEnvironment hostEnv;
        private string imageFolder = "/Images/";

        public FooterController(IFooterContactInfoRepository footerContactInfoRepo,
            IFooterSocialMediaRepository footerSocialMediaRepo, IWebHostEnvironment hostEnvironment)
        {
            footerContactInfoRepository = footerContactInfoRepo;
            footerSocialMediaRepository = footerSocialMediaRepo;
            hostEnv = hostEnvironment;
        }

        public IActionResult Index()
        {
            var footerContactInfoList = footerContactInfoRepository.FooterContactInfo;
            var footerSocialMediaList = footerSocialMediaRepository.FooterSocialMedias;

            FooterModel footerModel = new FooterModel()
            {
                FooterContactInfo = footerContactInfoList,
                FooterSocialMedias = footerSocialMediaList
            };
            return View(footerModel);
        }

        public async Task<IActionResult> Update(int? id, string? operation, bool? show, string? path)
        {
            ViewBag.contactInfoMessage = TempData["AdminFooterContactInfoMessage"];
            ViewBag.socialMediaMessage = TempData["AdminFooterSocialMediaMessage"];
            setViewBag(false, false, false, false);

            if (operation != null && show != null)
            {
                switch (operation)
                {
                    case "contactEdit":
                        ViewBag.contactEdit = show;
                        break;
                    case "add":
                        ViewBag.add = show;
                        break;
                    case "edit":
                        ViewBag.edit = show;
                        break;
                    case "delete":
                        ViewBag.delete = show;
                        break;
                }
            }

            FooterModel footerModel = new FooterModel();
            footerModel.FooterContactInfo = footerContactInfoRepository.FooterContactInfo;
            footerModel.FooterSocialMedias = footerSocialMediaRepository.FooterSocialMedias;
            if (id != null && operation != "add")
            {
                footerModel.FooterContact = footerContactInfoRepository.FooterContactInfo.FirstOrDefault(s => s.FooterContactInfoID == id);
                footerModel.FooterSocialMedia = footerSocialMediaRepository.FooterSocialMedias.FirstOrDefault(s => s.FooterSocialMediaID == id);
            }

            return View(footerModel);
        }

        public IActionResult EditFooterContactInfo(FooterModel footerModel)
        {
            if (ModelState.IsValid)
            {
                footerContactInfoRepository.Update(footerModel.FooterContact);
                TempData["AdminFooterContactInfoMessage"] = "Successfully edited Contact Info.";
            }
            else
            {
                footerModel.FooterContactInfo = footerContactInfoRepository.FooterContactInfo;
                footerModel.FooterSocialMedias = footerSocialMediaRepository.FooterSocialMedias;
                setViewBag(true, false, false, false);
                return View("Update", footerModel);
            }
            return RedirectToAction("Update");
        }

        public async Task<IActionResult> AddSocialMedia(FooterModel footerModel)
        {
            if (ModelState.IsValid)
            {
                footerModel.FooterSocialMedia.SocialMediaLogo = await SaveImage(footerModel.FooterSocialMedia.ImageFile);
                footerSocialMediaRepository.Create(footerModel.FooterSocialMedia);
            }
            else
            {
                footerModel.FooterContactInfo = footerContactInfoRepository.FooterContactInfo;
                footerModel.FooterSocialMedias = footerSocialMediaRepository.FooterSocialMedias;
                setViewBag(false, true, false, false);
                return View("Update", footerModel);
            }
            return RedirectToAction("Update");
        }

        public async Task<IActionResult> EditSocialMedia(FooterModel footerModel)
        {
            if (ModelState.IsValid)
            {
                DeleteImage(footerModel.FooterSocialMedia.SocialMediaLogo);
                footerModel.FooterSocialMedia.SocialMediaLogo = await SaveImage(footerModel.FooterSocialMedia.ImageFile);
                footerSocialMediaRepository.Edit(footerModel.FooterSocialMedia);
                TempData["AdminFooterSocialMediaMessage"] = "Successfully edited Social Media.";
            }
            else
            {
                footerModel.FooterContactInfo = footerContactInfoRepository.FooterContactInfo;
                footerModel.FooterSocialMedias = footerSocialMediaRepository.FooterSocialMedias;
                setViewBag(false, false, true, false);
                return View("Update", footerModel);
            }
            return RedirectToAction("Update");
        }

        public IActionResult DeleteSocialMedia(FooterModel footerModel)
        {
            DeleteImage(footerModel.FooterSocialMedia.SocialMediaLogo);
            footerSocialMediaRepository.Delete(footerModel.FooterSocialMedia);
            TempData["AdminFooterSocialMediaMessage"] = "Successfully deleted Social Media.";
            return RedirectToAction("Update");
        }

        public void setViewBag(bool contactEdit, bool add, bool edit, bool delete)
        {
            ViewBag.contactEdit = contactEdit;
            ViewBag.add = add;
            ViewBag.edit = edit;
            ViewBag.delete = delete;
        }

        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = Path.GetFileNameWithoutExtension(imageFile.FileName) +
                                        DateTime.Now.ToString("yyMMddssffff") +
                                        Path.GetExtension(imageFile.FileName);
            string imagePath = Path.Combine(hostEnv.WebRootPath + imageFolder, imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

        public void DeleteImage(string imageName)
        {
            string imagePath = Path.Combine(hostEnv.WebRootPath + imageFolder, imageName);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }
    }
}

