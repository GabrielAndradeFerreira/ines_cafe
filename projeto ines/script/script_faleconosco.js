const perguntas = document.querySelectorAll('.faq-question');

  perguntas.forEach(pergunta => {
    pergunta.addEventListener('click', () => {
      const item = pergunta.parentElement;
      item.classList.toggle('active');
    });
  });