// Espera o carregamento do DOM
document.addEventListener("DOMContentLoaded", () => {
  // -------------------------------
  // ELEMENTOS PRINCIPAIS
  // -------------------------------
  const corpo = document.body;
  const botaoContraste = document.getElementById("alternar-contraste");
  const botaoAumentar = document.getElementById("aumentar-texto");
  const botaoDiminuir = document.getElementById("diminuir-texto");
  const botaoPretoBranco = document.getElementById("preto-e-branco");
  const botaoVoz = document.getElementById("comando-de-voz");
  const botaoDark = document.getElementById("toggleTema");
  const botaoAlerta = document.getElementById("alerta");
  const menuAcessibilidade = document.querySelector(".teste > li");

  let tamanhoPadrao = 100; // tamanho base da fonte (%)
  let pretoBrancoAtivo = false;

  // -------------------------------
  // MENU DROPDOWN DE ACESSIBILIDADE
  // -------------------------------
  menuAcessibilidade.addEventListener("click", () => {
    const dropdown = menuAcessibilidade.querySelector(".dropdown");
    dropdown.classList.toggle("ativo");
  });

  // Fecha o menu se clicar fora
  document.addEventListener("click", (e) => {
    if (!menuAcessibilidade.contains(e.target)) {
      const dropdown = menuAcessibilidade.querySelector(".dropdown");
      dropdown.classList.remove("ativo");
    }
  });

  // -------------------------------
  // MODO ESCURO
  // -------------------------------
  botaoDark.addEventListener("click", () => {
    corpo.classList.toggle("modo-escuro");
  });

  // -------------------------------
  // ALERTA DE AJUDA
  // -------------------------------
  botaoAlerta.addEventListener("click", () => {
    alert("👋 Bem-vindo(a)! Use os ícones de acessibilidade para ajustar o contraste, o tamanho do texto e outras funções visuais.");
  });

  // -------------------------------
  // CONTRASTE ALTO
  // -------------------------------
  botaoContraste.addEventListener("click", () => {
    corpo.classList.toggle("contraste-alto");
  });

  // função de aumentar e diminuir
  function ajustarTexto(fator) {
  const elementos = document.querySelectorAll("p, h1, h2, h3, h4, h5, h6, a, li, span, label, button");
  elementos.forEach((el) => {
    const tamanhoAtual = window.getComputedStyle(el).fontSize;
    const novoTamanho = parseFloat(tamanhoAtual) * fator;
    el.style.fontSize = novoTamanho + "px";
  });
}

  // Aumentar texto
  botaoAumentar.addEventListener("click", () => {
    ajustarTexto(1.1); // aumenta 10%
  });

  // Diminuir texto
  botaoDiminuir.addEventListener("click", () => {
    ajustarTexto(0.9); // diminui 10%
  });

  // -------------------------------
  // PRETO E BRANCO
  // -------------------------------
  botaoPretoBranco.addEventListener("click", () => {
    pretoBrancoAtivo = !pretoBrancoAtivo;
    corpo.style.filter = pretoBrancoAtivo ? "grayscale(100%)" : "none";
  });

  // -------------------------------
  // COMANDO DE VOZ (simulado)
  // -------------------------------
  botaoVoz.addEventListener("click", () => {
    alert("🎙️ Função de comando de voz em desenvolvimento.");
  });
});


  // -------------------------------
  // HOME
  // -------------------------------
document.addEventListener('DOMContentLoaded', () => {
    const menuToggle = document.querySelector('.menu-toggle');
    const navMenu = document.getElementById('navMenu');

    if (menuToggle && navMenu) {
        menuToggle.addEventListener('click', () => {
            navMenu.classList.toggle('active');
        });
    }

    const navLinks = navMenu.querySelectorAll('a');
    navLinks.forEach(link => {
        link.addEventListener('click', () => {
            if (navMenu.classList.contains('active')) {
                navMenu.classList.remove('active');
            }
        });
    });

});

// CARROSSEL DO HOME /////
//document.addEventListener( 'DOMContentLoaded', function () {
//  new Splide( '#card-carousel', {
//		perPage    : 2,
//		breakpoints: {
//			640: {
//				perPage: 1,
//			},
//		},
//  } ).mount();
//} );