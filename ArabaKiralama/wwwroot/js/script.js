document.addEventListener("DOMContentLoaded", function () {
    const categoryCards = document.querySelectorAll(".category-card");
    const rentalCards = document.querySelectorAll(".rental-card");
    let activeCategory = null; // Şu an seçili olan kategoriyi takip etmek için

    categoryCards.forEach((categoryCard) => {
        categoryCard.addEventListener("click", () => {
            const selectedBrand = categoryCard.querySelector("img").alt.trim().toLowerCase();

            if (activeCategory === selectedBrand) {
                // Eğer zaten seçili olan kategoriye tıklandıysa, her şeyi geri yükle
                rentalCards.forEach((card) => {
                    card.style.display = "block";
                });
                activeCategory = null; // Aktif kategori sıfırlanır
            } else {
                // Yeni bir kategori seçildiğinde önce tüm kartları gizle
                rentalCards.forEach((card) => {
                    card.style.display = "none";
                });

                // Seçilen kategorinin araçlarını göster
                rentalCards.forEach((card) => {
                    const carBrand = card.querySelector("h3").textContent.trim().toLowerCase();
                    if (carBrand.includes(selectedBrand)) {
                        card.style.display = "block";
                    }
                });

                activeCategory = selectedBrand; // Seçilen kategoriyi aktif kategori olarak kaydet
            }
        });
    });
});