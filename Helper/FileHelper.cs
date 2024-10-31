namespace CinameManageMent.Helper
{
    public class FileHelper
    {
        public static string GenerateFileName(string path)
        {
            var name=Guid.NewGuid().ToString().Replace("-","");
            var lastIndex=path.LastIndexOf('.');
            var ext=path.Substring(lastIndex);
            return name + ext;
        }
        public static string GenerateFile(string path)
        {
            var name = Guid.NewGuid().ToString().Replace("-", "");
            var lastIndex = path.LastIndexOf('.');

            // Check if the lastIndex is valid
            if (lastIndex < 0)
            {
                // Handle the case where there is no extension
                // You can return a default extension or throw an exception
                return name + ".png"; // or any other default extension you prefer
            }

            var ext = path.Substring(lastIndex); // Get the extension
            return name + ext;
        }
        public static string GenerateFileVideo(string path)
        {
            var name = Guid.NewGuid().ToString().Replace("-", "");
            var lastIndex = path.LastIndexOf('.');

            if (lastIndex < 0)
            {
                
                return name + ".mp4"; 
            }

            var ext = path.Substring(lastIndex); 
            var validVideoExtensions = new[] { ".mp4", ".avi", ".mkv", ".mov", ".wmv", ".flv", ".webm" };

            // Validate if the extension is among the allowed formats
            if (!validVideoExtensions.Contains(ext.ToLower()))
            {
               
                return name + ".mp4"; 
            }

            return name + ext; 
        }

    }
}
