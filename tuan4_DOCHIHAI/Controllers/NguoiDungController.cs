﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tuan4_DOCHIHAI.Models;

namespace tuan4_DOCHIHAI.Controllers
{
    public class NguoiDungController : Controller
    {
        DataClasses1DataContext data = new DataClasses1DataContext();
        // GET: NguoiDung
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KhachHang kh)
        {
            var hoten = collection["hoten"];
            var tendangnhap = collection["tendangnhap"];
            var matkhau = collection["matkhau"];
            var MatKhauXacNhan = collection["MatKhauXacNhan"];
            var email = collection["email"];
            var diachi = collection["diachi"];
            var dienthoai = collection["dienthoai"];
            var ngaysinh = String.Format("{0:MM/dd/yyy}", collection["ngaysinh"]);

            if (String.IsNullOrEmpty(MatKhauXacNhan))
            {
                ViewData["NhapMKXN"] = "phải nhập mật khẩu xác nhận!";
            }
            else
            {
                if(!matkhau.Equals(MatKhauXacNhan))
                {
                    ViewData["MatKhauGiongNhau"] = "Mật khẩu và mật khẩu xác nhận phải giống nhau";
                }
                else
                {
                    kh.hoten = hoten;
                    kh.tendangnhap = tendangnhap;
                    kh.matkhau = matkhau;
                    kh.diachi = diachi;
                    kh.dienthoai = dienthoai;
                    kh.ngaysinh = DateTime.Parse(ngaysinh);

                    data.KhachHangs.InsertOnSubmit(kh);
                    data.SubmitChanges();

                    return RedirectToAction("DangNhap");
                }
            }
            return this.DangKy();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var tendangnhap = collection["tendangnhap"];
            var matkhau = collection["matkhau"];
            KhachHang kh = data.KhachHangs.SingleOrDefault(n => n.tendangnhap == tendangnhap && n.matkhau == matkhau);
            if (kh != null)
            {
                ViewBag.ThongBao = "Đăng nhập thành công ";
                Session["TaiKhoan"] = kh;
            }
            else
            {
                ViewBag.ThongBao = " tên đang nhập hoặc mật khẩu không đúng";
            }
            return RedirectToAction("Index", "Home");
        }
    }
}