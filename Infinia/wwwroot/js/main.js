// JavaScript for video slider effect
const buttons = document.querySelectorAll(".content__main-slider-btn");
const slides = document.querySelectorAll(".content__main-video");
const contents = document.querySelectorAll(".content__main-info");

var sliderNav = function(manual){
    buttons.forEach((btn) => {
        btn.classList.remove("active");
    });

    slides.forEach((slide) => {
        slide.classList.remove("active");
    });

    contents.forEach((content) => {
        content.classList.remove("active");
    });

    buttons[manual].classList.add("active");
    slides[manual].classList.add("active");
    contents[manual].classList.add("active");
}

buttons.forEach((btn, i) => {
    btn.addEventListener("click", () => {
        sliderNav(i);
    });
});

