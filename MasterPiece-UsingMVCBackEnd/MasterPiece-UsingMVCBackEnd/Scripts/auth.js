function checkLoginStatus() {
    const loggedIn = localStorage.getItem('loggedIn');
    const userRole = localStorage.getItem('role');

    if (loggedIn) {
        if (userRole === 'admin') {
            document.getElementById('adminDashboardLink').style.display = 'inline';
            document.getElementById('in').style.display = 'none';
            document.getElementById('logout').style.display = 'inline';
        } else {
            document.getElementById('userDashboardLink').style.display = 'inline';
            document.getElementById('in').style.display = 'none';
            document.getElementById('logout').style.display = 'inline';
        }
    } else {
        document.getElementById('in').style.display = 'inline';
        document.getElementById('logout').style.display = 'none';
    }
}

function handleLogout() {
    localStorage.removeItem('loggedIn');
    localStorage.removeItem('role');
    window.location.href = 'index.html';
}

document.addEventListener('DOMContentLoaded', checkLoginStatus);
document.getElementById('logout').addEventListener('click', handleLogout);
