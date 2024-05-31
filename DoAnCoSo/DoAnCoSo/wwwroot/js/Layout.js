


function toggleMenu() {
    var menuItems = document.querySelectorAll('.container-menu-item-content');
    var content = document.querySelector('.container-content');
    menuItems.forEach(function (item) {
        if (item.style.display === 'block') {
            item.style.display = 'none';
            content.style.paddingLeft = '36px';
        } else {
            item.style.display = 'block';
            content.style.paddingLeft = '135px';
        }
    });
}