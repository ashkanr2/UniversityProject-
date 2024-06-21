document.addEventListener("DOMContentLoaded", function () {
    var images = document.querySelectorAll('.course-image');

    images.forEach(function (img) {
        var imageId = img.getAttribute('data-image-id');

        if (imageId) {
            fetch(`/api/images/${imageId}`)
                .then(response => response.text()) // Assuming the API returns a URL string
                .then(data => {
                    if (data) {
                        img.src = data;
                    } else {
                        img.src = 'assets/images/cources/cource-1.jpg'; // Fallback image if the response is empty

                    }
                })
                .catch(error => {
                    console.error('Error loading image:', error);
                    img.src = 'assets/images/cources/cource-1.jpg'; // Fallback image in case of error
                });
        } else {
            img.src = 'assets/images/cources/cource-1.jpg'; // Fallback image if imageId is not set
        }
    });
});
