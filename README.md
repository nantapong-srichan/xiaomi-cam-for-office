# Office Camera

ต้นแบบแอป desktop ด้วย C# + Avalonia สำหรับดู Xiaomi C400 ผ่าน go2rtc/Home Assistant

## เปิดใช้งาน

เปิดโฟลเดอร์นี้ด้วย Rider/Visual Studio หรือใช้:

```sh
dotnet restore
dotnet run
```

รองรับ macOS, Windows และ Linux เมื่อมี .NET 8 SDK

หมายเหตุ: Xiaomi C400 ใช้งานผ่าน Mi Home เป็นหลัก แอปจึงรับสตรีมจาก go2rtc/Home Assistant แทนการเก็บรหัสผ่าน Xiaomi โดยตรง
