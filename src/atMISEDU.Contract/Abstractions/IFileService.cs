using atMISEDU.Contract.DTOs;

namespace atMISEDU.Contract.Abstractions
{
    /// <summary>
    /// Service help to manipulate file 
    /// </summary>
    public interface IFileService
    {
        Task<string> UploadFileAsync(UploadFileRequest uploadFileRequest);
        string GetFullPathFileServer(string? filePath);

        /// <summary>
        /// Get file extension from base64 string
        /// </summary>
        /// <param name="base64Content">Base64 content string</param>
        /// <returns>Extension of file</returns>
        string GetFileExtensionFromBase64(string base64Content);

        /// <summary>
        /// Get full file path from root directory to file name
        /// </summary>
        /// <param name="relativePath">Relative path</param>
        /// <param name="fileName">File name</param>
        /// <returns>Full file path</returns>
        string GetFilePath(string relativePath, string fileName);

        /*
       /// <summary>
       /// Get relative path from directory to file name
       /// </summary>
       /// <param name="relativePath">Relative path</param>
       /// <param name="fileName"></param>
       /// <returns></returns>
       string GetRelativeFilePath(string relativePath, string fileName);

       /// <summary>
       /// Save file to root folder
       /// </summary>
       /// <param name="file">File to save</param>
       /// <param name="filePath">File path</param>
       /// <returns></returns>
       Task SaveFile(IFormFile file, string filePath);

       /// <summary>
       /// Convert base64 string to <see cref="IFormFile"/>
       /// </summary>
       /// <param name="base64Content">Base64 content</param>
       /// <param name="fileName">File name</param>
       /// <returns><see cref="IFormFile"/> converted from base64</returns>
       IFormFile ConvertBase64ToFormFile(string base64Content, string fileName);

       /// <summary>
       /// Check if file is permitted to upload and save to root
       /// </summary>
       /// <param name="extension">Extension of file</param>
       /// <returns>True if file is permitted, otherwise false</returns>
       bool IsExtensionPermitted(string extension);
       */
    }
}