const talepEtButton = document.getElementById('TalepEt');
const girisYapButton = document.getElementById('GirisYap');
const container = document.getElementById('container');

talepEtButton.addEventListener('click', () => {
	container.classList.add("right-panel-active");
});

girisYapButton.addEventListener('click', () => {
	container.classList.remove("right-panel-active");
});