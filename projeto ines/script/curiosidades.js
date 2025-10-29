document.addEventListener('DOMContentLoaded', () => {
    // 1. Elementos do Modal
    const videoModal = document.getElementById('videoModal');
    const videoFrame = document.getElementById('videoFrame');
    const closeModal = document.querySelector('.modal .close');

    // 2. Elementos que disparam a abertura do modal
    const videoTriggers = document.querySelectorAll('.open-video-trigger');

    videoTriggers.forEach(trigger => {
        trigger.addEventListener('click', (e) => {
            e.preventDefault(); // Impede o comportamento padrão do link (#)

            // Encontra o card pai para pegar o ID do vídeo
            const card = trigger.closest('.video-card2');
            const videoId = card ? card.getAttribute('data-video-id') : null;

            if (videoId) {
                // Monta a URL de incorporação do YouTube com autoplay
                const embedUrl = `https://www.youtube.com/embed/${videoId}?autoplay=1&rel=0`;
                
                videoFrame.src = embedUrl;
                videoModal.style.display = 'block';
            }
        });
    });
    
    // 3. Lógica para fechar o modal e parar o vídeo
    const closeAndStopVideo = () => {
        videoModal.style.display = 'none';
        videoFrame.src = ''; // Limpar o src do iframe para parar a reprodução
    };

    if (closeModal) {
        closeModal.addEventListener('click', closeAndStopVideo);
    }

    // Fecha o modal ao clicar fora dele
    window.addEventListener('click', (event) => {
        if (event.target === videoModal) {
            closeAndStopVideo();
        }
    });
});