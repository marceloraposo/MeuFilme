// *** Change Navigation Background ***
window.addEventListener('scroll', () => {
    let scroll = window.scrollY;
    if (scroll >= 150) {
        document.querySelector('#navigation').classList.add('change-back-nav');
    } 
    else {
        document.querySelector('#navigation').classList.remove('change-back-nav');
    }
});

// *** Smooth Scrolling ***
$('a.about-btn, a.link-top').on('click', function(event) {
    if (this.hash !== '') {
      event.preventDefault();
      const hash = this.hash;    
      $('html, body').animate(
        {
          scrollTop: $(hash).offset().top - 95
        }, 900
      );
    }
});

// *** Main Menu button ***
let mainMenuBtn = document.querySelector('.main-menu-btn');
let sideNav = document.querySelector('.side-nav');

mainMenuBtn.addEventListener('click', () => {
  mainMenuBtn.classList.toggle('rotate-line');
  sideNav.classList.toggle('side-nav-change');
});

// *** Date ***
let date = new Date();
let year = date.getFullYear();
let month = date.getMonth();

let monthArray = new Array("January","February","March","April","May","June","July","August","September","October","November","December");

let showDate = document.getElementById('date');
// showDate.innerHTML = `${monthArray[month]} / ${year}`;
showDate.textContent = year;