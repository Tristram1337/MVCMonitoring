var images = Array.from(document.querySelectorAll('.img-fluid'));
var overlayImage = document.querySelector('#overlayImage');
var overlay = document.querySelector('.overlay');
var currentIndex;

images.forEach(function (image, index) {
    image.addEventListener('click', function () {
        overlayImage.src = this.src;
        overlay.style.display = 'flex';
        currentIndex = index;
    });
});

function changeImage(direction) {
    currentIndex = (currentIndex + direction + images.length) % images.length;
    overlayImage.src = images[currentIndex].src;
}

function hideOverlay() {
    overlay.style.display = 'none';
}

window.addEventListener('keydown', function (event) {
    if (event.key === 'ArrowRight') {
        changeImage(1);
    } else if (event.key === 'ArrowLeft') {
        changeImage(-1);
    } else if (event.key === 'Escape') {
        hideOverlay();
    }
});