using atMISEDU.Contract.Abstractions;
using atMISEDU.Contract.Constants;
using atMISEDU.Contract.DTOs;
using atMISEDU.Contract.Enumerations;
using atMISEDU.Contract.Exceptions;
using atMISEDU.Contract.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.IO.Compression;

namespace atMISEDU.Contract.Services
{
    public class FileService : IFileService
    {
        private readonly string fileDirectoryPath;
        private readonly DomainHostsDTOs domainSetting;
        private readonly UploadedSettingsDTOs uploadedSettings;

        private const long MAX_FILE_SIZE = 2 * 1024 * 1024;

        // use data from db
        private readonly List<string> permittedExtensions = [".png", ".jpg", ".jpeg", ".pdf", ".webp", ".doc", ".docx", ".xlsx"];

        private static readonly List<FileSignature> FileSignatures =
        [
            new(".jpeg", [0xFF, 0xD8]), // JPEG
            new(".png", [0x89, 0x50, 0x4E, 0x47]), // PNG
            new(".pdf", [0x25, 0x50, 0x44, 0x46]), // PDF
            new(".gif", [0x47, 0x49, 0x46, 0x38]), // GIF
            new(".webp", [
                0x52, 0x49, 0x46, 0x46, // RIFF
                null, null, null, null, // xx xx xx xx (bỏ qua)
                0x57, 0x45, 0x42, 0x50 // WEBP
            ]),
            new(".doc", [0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1]), // Word (.doc)
            new(".docx", [0x50, 0x4B, 0x03, 0x04]) // Word (.docx)
        ];

        public FileService(IWebHostEnvironment env,
                           IOptions<DomainHostsDTOs> domainSetting,
                           IOptions<UploadedSettingsDTOs> uploadedSettings)
        {
            fileDirectoryPath = Path.Combine(env.WebRootPath);
            this.domainSetting = domainSetting.Value;
            this.uploadedSettings = uploadedSettings.Value;
        }

        public string GetFileExtensionFromBase64(string base64Content)
        {
            byte[] fileBytes = Convert.FromBase64String(base64Content);

            var signature = FileSignatures.FirstOrDefault(s =>
                fileBytes.Length >= s.Signature.Length &&
                MatchesSignature(fileBytes, s.Signature));

            if (signature == null)
                throw new InvalidOperationException("Unknown file format.");

            // Không phải ZIP → return luôn
            if (signature.Extension != ".docx")
                return signature.Extension;

            // ZIP (docx / xlsx / pptx) → phân biệt bên trong
            using var ms = new MemoryStream(fileBytes);
            using var zip = new ZipArchive(ms, ZipArchiveMode.Read);

            if (zip.Entries.Any(e => e.FullName.StartsWith("xl/")))
                return ".xlsx";

            if (zip.Entries.Any(e => e.FullName.StartsWith("word/")))
                return ".docx";

            if (zip.Entries.Any(e => e.FullName.StartsWith("ppt/")))
                return ".pptx";

            throw new InvalidOperationException("Unknown Office document format.");
        }

        public string GetFilePath(string relativePath, string fileName)
        {
            // Combine directory with relative path
            string fullRelativePath = Path.Combine(fileDirectoryPath, relativePath);

            // If directory after combine isn't exist, create new   
            if (!Directory.Exists(fullRelativePath)) Directory.CreateDirectory(fullRelativePath);

            // Combine with file name to get full file path
            return Path.Combine(fullRelativePath, fileName).Replace("\\", "/");
        }

        private string GetRelativeFilePath(string relativePath, string fileName) => Path.Combine(relativePath, fileName).Replace("\\", "/");

        private async Task SaveFile(IFormFile file, string filePath)
        {
            // Write file to directory
            await using FileStream stream = new(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
        }

        private IFormFile ConvertBase64ToFormFile(string base64Content, string fileName)
        {
            byte[]? fileBytes = Convert.FromBase64String(base64Content);
            if (fileBytes == null || fileBytes.Length == 0) CustomException.ThrowValidationException(MsgCode.ERR_INVALID_FILE);
            if (fileBytes.Length > MAX_FILE_SIZE)
                CustomException.ThrowValidationException(MsgCode.ERR_FILE_TOO_LARGE);
            MemoryStream stream = new(fileBytes);
            return new FormFile(stream, 0, fileBytes.Length, "file", fileName);
        }

        private bool IsExtensionPermitted(string extension) => permittedExtensions.Contains(extension);

        private static bool MatchesSignature(byte[] fileBytes, byte?[] signature)
        {
            for (int i = 0; i < signature.Length; i++)
            {
                // Skip null
                if (signature[i] == null)
                    continue;
                if (fileBytes[i] != signature[i])
                    return false;
            }

            return true;
        }

        public async Task<string> UploadFileAsync(UploadFileRequest uploadFileRequest)
        {
            string pathResult = string.Empty;
            try
            {
                uploadFileRequest.RelativePath = GetPathDir(uploadFileRequest.enumOptionPath, uploadedSettings);

                //UploadFileRequest uploadFileRequest = context.Message;
                string extension = GetFileExtensionFromBase64(uploadFileRequest.Content);
                string fileName = Path.GetFileNameWithoutExtension(uploadFileRequest.FileName) + extension;

                // Check extension is permitted
                if (!IsExtensionPermitted(extension))
                    CustomException.ThrowValidationException(MsgCode.ERR_INVALID_FILE, MsgConst.EXT_NOT_PERMITTED);

                IFormFile file = ConvertBase64ToFormFile(uploadFileRequest.Content, fileName);

                string filePath = GetFilePath(uploadFileRequest.RelativePath, fileName);

                // Write file to directory
                await SaveFile(file, filePath);

                pathResult = GetRelativeFilePath(uploadFileRequest.RelativePath, fileName);
            }
            catch (Exception)
            {
                pathResult = "";
            }

            return pathResult;
        }

        private string GetPathDir(EnumOptionPath optionPathEnum, UploadedSettingsDTOs settings)
        {
            string res = Const.OUTPUTFILE_UPLOADED;

            switch (optionPathEnum)
            {
                case EnumOptionPath.ConfigIcon:
                    res = checkNullorEmpty(settings.ConfigIcon, res);
                    break;

                case EnumOptionPath.UploadedFile:
                    res = checkNullorEmpty(settings.UploadedFile, res);
                    break;

                case EnumOptionPath.Account:
                    res = checkNullorEmpty(settings.Account, res);
                    break;

                case EnumOptionPath.MenuCatalog:
                    res = checkNullorEmpty(settings.MenuCatalog, res);
                    break;

                case EnumOptionPath.ContractFile:
                    res = checkNullorEmpty(settings.ContractFile, res);
                    break;
            }

            return res;
        }

        private string checkNullorEmpty(string values, string _default = "")
        {
            if (!string.IsNullOrEmpty(values)) return values;
            return _default;
        }

        public string GetFullPathFileServer(string? filePath)
        {
            return !string.IsNullOrWhiteSpace(filePath) ? string.Format("{0}{1}", domainSetting.Url, filePath) : null;
        }
    }
}