


function toggleMenu() {
    var menuItems = document.querySelectorAll('.container-menu-item-content');

    menuItems.forEach(function (item) {
        if (item.style.display === 'block') {
            item.style.display = 'none';
        } else {
            item.style.display = 'block';
        }
    });
}