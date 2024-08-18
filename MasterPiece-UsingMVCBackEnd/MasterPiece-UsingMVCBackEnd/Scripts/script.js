document.addEventListener("DOMContentLoaded", function() {
    var modal = document.getElementById("modal");
    var btn = document.getElementById("modalBtn");

    // Show the modal when the button is clicked
    btn.onclick = function() {
        modal.classList.add("show");
    }

    // Close the modal when clicking outside of the modal content
    window.onclick = function(event) {
        if (event.target == modal) {
            modal.classList.remove("show");
        }
    }
});
