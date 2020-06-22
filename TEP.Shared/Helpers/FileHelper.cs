using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TEP.Shared.Helpers
{
    /// <summary>
    /// Helper Class to deal with File Manipulation and Validation.
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Process an IFormFIle, validating its information and saving it on Disk. 
        /// Shoul search for virus and analyse byte to match the extension (NOT IMPLEMMENTED).
        /// </summary>
        /// <param name="data">The File to be validated and processed</param>
        /// <param name="permittedExtensions">Extions accepted by the application</param>
        /// <param name="modelState">A ModelState from the Caller, where errors are added</param>
        /// <param name="sizeLimit">Maximum size in bytes for this file</param>
        /// <param name="path">Path where the file should be saved</param>
        /// <param name="fileName"> The of the file to be used when saving to disk</param>
        /// <returns></returns>
        public static async Task ProcessAndValidateFile(IFormFile data, string[] permittedExtensions, long sizeLimit, string path, string fileName)
        {

            if (!FileHelper.IsFileExtensionValid(data.FileName, permittedExtensions))
                throw new ArgumentOutOfRangeException("File Error", $"File type is not Accepeted. Use {permittedExtensions}");

            if (data.Length == 0)
                throw new FileNotFoundException("File Error", $"File is empty.");

            if (data.Length > sizeLimit)
                throw new ArgumentOutOfRangeException("File Error", $"File exceeds Maximum Size of {sizeLimit}.");

            var filePath = FileHelper.CombinePathAndName(path, fileName);

            try
            {
                using (var stream = System.IO.File.Create(filePath))
                {
                    await data.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                throw new IOException($"upload failed. Please contact the Help Desk for support. Error: {ex.HResult}");
            }
        }

        /// <summary>
        /// Verifies if the file extension is compatible with the lsit passed (using only the file extension). 
        /// </summary>
        /// <param name="fileName"> The name of the File</param>
        /// <param name="permittedExtensions"> Allowed Extensions </param>
        /// <returns>Returns if the extension is supported or not.</returns>
        public static bool IsFileExtensionValid( string fileName, string[] permittedExtensions)
        {
            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
                return false;

            return true;
        }

        /// <summary>
        /// Generates an Unique FileName in Format: key_yyyyMMddHHmmssfff_Guid.ext
        /// </summary>
        /// <param name="key"> A keyword used to contextualize this file</param>
        /// <param name="originalName"> name from which we can extract its extension </param>
        /// <returns> An string with the unique name</returns>
        public static string GetNewUniqueName(string key, string originalName)
        {
            var ext = Path.GetExtension(originalName).ToLowerInvariant();
            return key + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + Guid.NewGuid().ToString("N") + ext;
        }

        /// <summary>
        /// Combines Path and file name.
        /// </summary>
        /// <param name="filePath"> Path where the FIle should be Saved. </param>
        /// <param name="fileName"> Name to be used to save the file. </param>
        /// <returns></returns>
        public static string CombinePathAndName(string filePath, string fileName)
        {            
            return Path.Combine(filePath, fileName); ;
        }

        /// <summary>
        /// Deletes a File.
        /// </summary>
        /// <param name="filePath">The path to the file to be deleted.</param>
        public static void DeleteFromDisk(string filePath)
        {
            try
            {
                System.IO.File.Delete(filePath);
            }
            catch (Exception)
            {
                throw new IOException("File Could not be deleted");
            }

        }

    }
}
