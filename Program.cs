using System.Globalization;
using System.Text;
using Lab03_QuanLySinhVienOOP;

internal class Program
{
    private static readonly QuanLySinhVien _quanLy = new();

    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        bool tiepTuc = true;
        while (tiepTuc)
        {
            HienThiMenu();
            string luaChon = Console.ReadLine()?.Trim() ?? string.Empty;
            Console.WriteLine();

            switch (luaChon)
            {
                case "1": ThemSinhVien(); break;
                case "2": XuatDanhSach(); break;
                case "3": TimTheoMa(); break;
                case "4": TimTheoTen(); break;
                case "5": SuaDiem(); break;
                case "6": XoaSinhVien(); break;
                case "7": SapXepTheoDiem(); break;
                case "8": LocSinhVienDat(); break;
                case "0":
                    Console.WriteLine("Tạm biệt!");
                    tiepTuc = false;
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ, vui lòng nhập lại.");
                    break;
            }

            if (tiepTuc)
            {
                Console.WriteLine();
            }
        }
    }

    // ===================== MENU =====================

    private static void HienThiMenu()
    {
        Console.WriteLine("===== QUAN LY SINH VIEN =====");
        Console.WriteLine("1. Them sinh vien");
        Console.WriteLine("2. Xuat danh sach");
        Console.WriteLine("3. Tim sinh vien theo ma");
        Console.WriteLine("4. Tim sinh vien theo ten");
        Console.WriteLine("5. Sua diem trung binh");
        Console.WriteLine("6. Xoa sinh vien");
        Console.WriteLine("7. Sap xep theo diem giam dan");
        Console.WriteLine("8. Loc sinh vien dat");
        Console.WriteLine("0. Thoat");
        Console.Write("Chon chuc nang: ");
    }

    // ===================== CÁC CHỨC NĂNG =====================

    private static void ThemSinhVien()
    {
        Console.WriteLine("--- THÊM SINH VIÊN ---");

        string ma = NhapChuoi("Nhập mã sinh viên: ");
        if (_quanLy.TimTheoMa(ma) != null)
        {
            Console.WriteLine($"Mã sinh viên {ma.ToUpper()} đã tồn tại, không thể thêm.");
            return;
        }

        string hoTen = NhapChuoi("Nhập họ tên: ");
        DateTime ngaySinh = NhapNgaySinh("Nhập ngày sinh (dd/MM/yyyy): ");
        string maLop = NhapChuoi("Nhập mã lớp: ");
        double diem = NhapDiem("Nhập điểm trung bình (0 - 10): ");

        var sinhVien = new SinhVien(ma, hoTen, ngaySinh, maLop, diem);
        if (_quanLy.Them(sinhVien))
        {
            Console.WriteLine($"Thêm thành công! Xếp loại: {sinhVien.XepLoai()}.");
        }
        else
        {
            Console.WriteLine("Mã sinh viên đã tồn tại, không thể thêm.");
        }
    }

    private static void XuatDanhSach()
    {
        Console.WriteLine("--- DANH SÁCH SINH VIÊN ---");
        InBang(_quanLy.LayDanhSach());
    }

    private static void TimTheoMa()
    {
        Console.WriteLine("--- TÌM THEO MÃ ---");
        string ma = NhapChuoi("Nhập mã sinh viên cần tìm: ");
        SinhVien? sinhVien = _quanLy.TimTheoMa(ma);

        if (sinhVien == null)
        {
            Console.WriteLine("Không tìm thấy sinh viên có mã này.");
            return;
        }
        Console.WriteLine(sinhVien.LayThongTin());
    }

    private static void TimTheoTen()
    {
        Console.WriteLine("--- TÌM THEO TÊN ---");
        string tuKhoa = NhapChuoi("Nhập từ khóa họ tên: ");
        List<SinhVien> ketQua = _quanLy.TimTheoTen(tuKhoa);

        if (ketQua.Count == 0)
        {
            Console.WriteLine("Không tìm thấy sinh viên nào phù hợp.");
            return;
        }
        InBang(ketQua);
    }

    private static void SuaDiem()
    {
        Console.WriteLine("--- SỬA ĐIỂM TRUNG BÌNH ---");
        string ma = NhapChuoi("Nhập mã sinh viên cần sửa điểm: ");
        SinhVien? sinhVien = _quanLy.TimTheoMa(ma);

        if (sinhVien == null)
        {
            Console.WriteLine("Không tìm thấy sinh viên có mã này.");
            return;
        }

        Console.WriteLine($"Sinh viên: {sinhVien.HoTen} - Điểm hiện tại: {sinhVien.DiemTrungBinh:0.00}");
        double diemMoi = NhapDiem("Nhập điểm mới (0 - 10): ");
        _quanLy.SuaDiem(ma, diemMoi);
        Console.WriteLine($"Cập nhật thành công! Xếp loại mới: {sinhVien.XepLoai()}.");
    }

    private static void XoaSinhVien()
    {
        Console.WriteLine("--- XÓA SINH VIÊN ---");
        string ma = NhapChuoi("Nhập mã sinh viên cần xóa: ");

        if (_quanLy.Xoa(ma))
        {
            Console.WriteLine("Đã xóa sinh viên.");
        }
        else
        {
            Console.WriteLine("Không tìm thấy sinh viên có mã này.");
        }
    }

    private static void SapXepTheoDiem()
    {
        Console.WriteLine("--- DANH SÁCH SẮP XẾP THEO ĐIỂM GIẢM DẦN ---");
        InBang(_quanLy.SapXepTheoDiem());
    }

    private static void LocSinhVienDat()
    {
        Console.WriteLine("--- SINH VIÊN ĐẠT (ĐIỂM TB >= 5) ---");
        InBang(_quanLy.LocSinhVienDat());
    }

    // ===================== HÀM IN =====================

    private static void InBang(List<SinhVien> danhSach)
    {
        if (danhSach.Count == 0)
        {
            Console.WriteLine("Danh sách trống.");
            return;
        }

        string tieuDe = $"{"Mã SV",-10}{"Họ tên",-28}{"Ngày sinh",-13}{"Lớp",-12}{"Điểm",-8}{"Xếp loại",-12}";
        Console.WriteLine(tieuDe);
        Console.WriteLine(new string('-', tieuDe.Length));

        foreach (SinhVien sv in danhSach)
        {
            Console.WriteLine(
                $"{sv.MaSinhVien,-10}{sv.HoTen,-28}{sv.NgaySinh,-13:dd/MM/yyyy}" +
                $"{sv.MaLop,-12}{sv.DiemTrungBinh,-8:0.00}{sv.XepLoai(),-12}");
        }
        Console.WriteLine($"Tổng: {danhSach.Count} sinh viên.");
    }

    // ===================== HÀM NHẬP DỮ LIỆU (có kiểm tra lỗi) =====================

    /// <summary>Nhập chuỗi không rỗng; nhập rỗng thì yêu cầu nhập lại.</summary>
    private static string NhapChuoi(string thongBao)
    {
        while (true)
        {
            Console.Write(thongBao);
            string? input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Trim();
            }
            Console.WriteLine("Giá trị không được để trống, vui lòng nhập lại.");
        }
    }

    /// <summary>Nhập ngày sinh theo định dạng dd/MM/yyyy, không được ở tương lai.</summary>
    private static DateTime NhapNgaySinh(string thongBao)
    {
        string[] dinhDang = { "dd/MM/yyyy", "d/M/yyyy" };

        while (true)
        {
            Console.Write(thongBao);
            string? input = Console.ReadLine();

            if (!DateTime.TryParseExact(input?.Trim(), dinhDang, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime ngaySinh))
            {
                Console.WriteLine("Ngày sinh không đúng định dạng dd/MM/yyyy, vui lòng nhập lại.");
                continue;
            }

            if (ngaySinh > DateTime.Today)
            {
                Console.WriteLine("Ngày sinh không được ở tương lai, vui lòng nhập lại.");
                continue;
            }
            return ngaySinh;
        }
    }

    /// <summary>Nhập điểm trong khoảng 0 - 10 (chấp nhận cả dấu '.' và ',').</summary>
    private static double NhapDiem(string thongBao)
    {
        while (true)
        {
            Console.Write(thongBao);
            string? input = Console.ReadLine()?.Trim().Replace(',', '.');

            if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double diem))
            {
                Console.WriteLine("Điểm phải là một số, vui lòng nhập lại.");
                continue;
            }

            if (double.IsNaN(diem) || diem < 0 || diem > 10)
            {
                Console.WriteLine("Điểm không hợp lệ (phải từ 0 đến 10), vui lòng nhập lại.");
                continue;
            }
            return diem;
        }
    }
}
