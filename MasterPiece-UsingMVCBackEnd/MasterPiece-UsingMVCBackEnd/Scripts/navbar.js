document.addEventListener('DOMContentLoaded', () => {
    checkLoginStatus();

    const logoutBtn = document.getElementById('logout');
    if (logoutBtn) {
        logoutBtn.addEventListener('click', () => {
            localStorage.removeItem('email');
            localStorage.removeItem('pass');
            updateNavbarForLoggedOutUser();
        });
    }
});

function checkLoginStatus() {
    const email = localStorage.getItem('email');
    const password = localStorage.getItem('pass');

    if (email && password) {
        updateNavbarForLoggedInUser();
    }
}

function updateNavbarForLoggedInUser() {
    const loginNav = document.getElementById('in');
    const logoutNav = document.getElementById('logout');

    if (loginNav && logoutNav) {
        loginNav.style.display = 'none';
        logoutNav.style.display = 'inline';
    }
}

function updateNavbarForLoggedOutUser() {
    const loginNav = document.getElementById('in');
    const logoutNav = document.getElementById('logout');

    if (loginNav && logoutNav) {
        loginNav.style.display = 'inline';
        logoutNav.style.display = 'none';
    }
}
