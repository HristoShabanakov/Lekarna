$(function () {
    const URL_PARTS = {
        OFFERS_ALL: "Offers/All",
        OFFERS_CREATE: "Offers/Create",
        CATEGORIES_ALL: "Categories/All",
        CATEGORIES_CREATE: "Categories/Create",
        PHARMACIES_ALL: "Pharmacies/All",
        PHARMACIES_CREATE: "Pharmacies/Create",
        SUPPLIERS_ALL: "Suppliers/All",
        SUPPLIERS_CREATE: "Suppliers/Create",
    }

    const url = window.location.href;
    const mainLinks = $('li[class*="nav-item has-treeview"]');

    [...mainLinks].forEach(x => {
        x.addEventListener('click', (e) => {
            if ($(x).hasClass('menu-open')) {
                $(x.children[0]).removeClass("active");

            } else {
                $(x.children[0]).addClass("active");
            }
        });
    });

    const expandMainNavLink = (id) => {
        $(id).addClass('menu-open');
    }

    const activateLinks = (links) => {
        links.forEach(link => {
            $(link).addClass('active');
        });
    }

    if (url.indexOf(URL_PARTS.OFFERS_ALL) >= 0) {
        expandMainNavLink('#offers');
        activateLinks(['#offer-all', '#offer'])
    } else if (url.indexOf(URL_PARTS.OFFERS_CREATE) >= 0) {
        expandMainNavLink('#offers');
        activateLinks(['#offer-create', '#offer'])
    } else if (url.indexOf(URL_PARTS.CATEGORIES_ALL) >= 0) {
        expandMainNavLink('#categories');
        activateLinks(['#category-all', '#category'])
    } else if (url.indexOf(URL_PARTS.CATEGORIES_CREATE) >= 0) {
        expandMainNavLink('#categories');
        activateLinks(['#category-create', '#category'])
    } else if (url.indexOf(URL_PARTS.SUPPLIERS_ALL) >= 0) {
        expandMainNavLink('#suppliers');
        activateLinks(['#supplier-all', '#supplier'])
    } else if (url.indexOf(URL_PARTS.SUPPLIERS_CREATE) >= 0) {
        expandMainNavLink('#suppliers');
        activateLinks(['#supplier-create', '#supplier'])
    } else if (url.indexOf(URL_PARTS.PHARMACIES_ALL) >= 0) {
        expandMainNavLink('#pharmacies');
        activateLinks(['#pharmacy-all', '#pharmacy'])
    } else if (url.indexOf(URL_PARTS.PHARMACIES_CREATE) >= 0) {
        expandMainNavLink('#pharmacies');
        activateLinks(['#pharmacy-create', '#pharmacy'])
    }
})
