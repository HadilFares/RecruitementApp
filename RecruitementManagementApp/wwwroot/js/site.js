// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
@helper ShowToast(string message, string title = "Notification", string type = "info")
{
    <script>
        toastr.options = {
            "closeButton": true,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "timeOut": "3000",
        };

        toastr.@type("@title", "@message");
    </script>
}
