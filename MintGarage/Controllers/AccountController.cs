﻿using Microsoft.AspNetCore.Mvc;
using MintGarage.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MintGarage.Models.Partners;

namespace MintGarage.Controllers
{
    public class AccountController : Controller
    {
        public IAccountRepository accoutRepository;
        public IPartnerRepository partnerRepository;

        public AccountController(IAccountRepository accountRepo, IPartnerRepository partnerRepo)
        {
            accoutRepository = accountRepo;
            partnerRepository = partnerRepo;
        }

        public IActionResult Login()
        {
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.Message = TempData["Message"];
            ViewBag.Success = TempData["Success"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Account account)
        {
            ViewBag.Partners = partnerRepository.Partners;
            if (ModelState.IsValid)
            {
                Account acc = accoutRepository.Account.FirstOrDefault();
                if (acc.Username == account.Username && acc.Password == account.Password)
                {
                    return RedirectToAction("Update", "Home");
                }
                else
                {
                    TempData["Message"] = "Invalid username or password. Please try again!";
                    TempData["Success"] = false;
                    return RedirectToAction("Login");
                }
            }
            return View(account);
        }

        public IActionResult Update()
        {
            ViewBag.Partners = partnerRepository.Partners;
            ViewBag.Message = TempData["Message"];
            ViewBag.Success = TempData["Success"];
            return View();
        }

        [HttpPost]
        public IActionResult Update(UpdatePassword updatePassword)
        {
            ViewBag.Partners = partnerRepository.Partners;
            if (ModelState.IsValid)
            {
                Account acc = accoutRepository.Account.FirstOrDefault();
                
                if (!acc.Password.Equals(updatePassword.CurrectPassword))
                {
                    TempData["Message"] = "Incorrect current password. Please try again!";
                    TempData["Success"] = false;
                }
                if (acc.Password.Equals(updatePassword.CurrectPassword))
                {
                    acc.Password = updatePassword.NewPassword;
                    accoutRepository.Update(acc);
                    TempData["Message"] = "Password updated successfully.";
                    TempData["Success"] = true;
                }
                return RedirectToAction("Update");
            }
            return View();
        }

        public IActionResult Logout()
        {
            ViewBag.Partners = partnerRepository.Partners;
            return RedirectToAction("index", "Home");
        }
    }
}
