
const courses = [
  {
    id: 1,
    title: "Curso Barista Essencial – INESCAFÉ",
    description: "Aprenda as técnicas fundamentais para preparar cafés de alta qualidade em casa ou no trabalho. Inclui moagem correta, métodos de preparo (coador, prensa francesa e moka) e noções de torra.",
    price: 249.90,
    category: "Barista",
    image: placeholderImage('#9b5b44', 'Curso Barista Essencial')
  },
  {
    id: 2,
    title: "Curso de Cafés Especiais & Harmonização – INESCAFÉ",
    description: "Um mergulho no universo dos cafés gourmets, explorando diferentes regiões produtoras, perfis sensoriais e combinações com doces, queijos e sobremesas.",
    price: 349.90,
    category: "Harmonização",
    image: placeholderImage('#5b3b2a', 'Cafés Especiais & Harmonização')
  },
  {
    id: 3,
    title: "Curso Avançado de Latte Art – INESCAFÉ",
    description: "Para quem já domina a base do café e deseja elevar a apresentação. Técnicas de vaporização do leite, desenhos clássicos (coração, tulipa, roseta) e prática intensiva.",
    price: 399.90,
    category: "Latte Art",
    image: placeholderImage('#6a2f1f', 'Latte Art Avançado')
  }
];

// estado da página
const state = {
  category: 'all',
  sortDir: 'desc' // 'desc' (maior->menor) | 'asc'
};

// elementos
const catalogEl = document.getElementById('catalogList');
const categorySelect = document.getElementById('categorySelect');
const sortBtn = document.getElementById('sortBtn');
const sortText = document.getElementById('sortText');
const toggleViewBtn = document.getElementById('toggleViewBtn');
const gridIcon = document.getElementById('gridIcon');
const listIcon = document.getElementById('listIcon');


// popula select de categorias
function buildCategoryOptions(){
  const cats = courses.map(c => c.category);
  const unique = Array.from(new Set(cats));
  unique.forEach(cat => {
    const opt = document.createElement('option');
    opt.value = cat;
    opt.textContent = cat;
    categorySelect.appendChild(opt);
  });
}

function renderCatalog(){
  // limpar
  catalogEl.innerHTML = '';

  // filtrar
  let list = courses.slice();
  if(state.category !== 'all'){
    list = list.filter(c => c.category === state.category);
  }

  // ordenar por preço
  list.sort((a,b) => state.sortDir === 'desc' ? b.price - a.price : a.price - b.price);

  list.forEach((c, idx) => {
    const row = document.createElement('article');
    row.className = 'course-row';
    row.setAttribute('data-id', c.id);

    const imgCol = document.createElement('div');
    imgCol.className = 'course-image';
    const inner = document.createElement('div');
    inner.className = 'img-inner';
    inner.style.backgroundImage = `url('${c.image}')`;

    const overlay = document.createElement('div');
    overlay.className = 'img-overlay-title';
    overlay.textContent = c.title.split('–')[0]; 
    inner.appendChild(overlay);
    imgCol.appendChild(inner);

    const contentCol = document.createElement('div');
    contentCol.className = 'course-content';

    const title = document.createElement('h3');
    title.className = 'course-title';
    title.innerHTML = `<span class="kicker">🔖</span> ${c.title}`;

    const desc = document.createElement('p');
    desc.className = 'course-desc';
    desc.textContent = `Descrição: ${c.description}`;

    const price = document.createElement('div');
    price.className = 'course-price';
    price.textContent = `Preço: ${formatPrice(c.price)}`;

    contentCol.appendChild(title);
    contentCol.appendChild(desc);
    contentCol.appendChild(price);

    row.appendChild(contentCol);
    row.appendChild(imgCol);

    catalogEl.appendChild(row);
  });
}

function formatPrice(v){
  return v.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
}

function placeholderImage(bgColor = '#7a4b3f', text = 'INESCAFÉ'){
  const w = 1200;
  const h = 700;
  const title = text.replace(/'/g, "\\'");
  const svg = `<svg xmlns='http://www.w3.org/2000/svg' width='${w}' height='${h}' viewBox='0 0 ${w} ${h}'>
    <defs>
      <linearGradient id='g' x1='0' x2='0' y1='0' y2='1'>
        <stop offset='0' stop-color='${bgColor}' stop-opacity='1'/>
        <stop offset='1' stop-color='${bgColor}' stop-opacity='0.9'/>
      </linearGradient>
    </defs>
    <rect width='100%' height='100%' fill='url(#g)'/>
    <g fill='#f7e8db' font-family='Playfair Display, serif' font-weight='700'>
      <text x='6%' y='36%' font-size='72'>${title}</text>
      <text x='6%' y='52%' font-size='36'>INESCAFÉ</text>
    </g>
  </svg>`;
  return 'data:image/svg+xml;utf8,' + encodeURIComponent(svg);
}

sortBtn.addEventListener('click', () => {
  state.sortDir = state.sortDir === 'desc' ? 'asc' : 'desc';
  sortText.textContent = state.sortDir === 'desc' ? 'Preço Maior ao menor' : 'Preço Menor ao maior';
  sortBtn.setAttribute('aria-pressed', state.sortDir === 'asc' ? 'true' : 'false');
  renderCatalog();
});


categorySelect.addEventListener('change', (e) => {
  state.category = e.target.value === 'all' ? 'all' : e.target.value;
  renderCatalog();
});

toggleViewBtn.addEventListener('click', () => {
  const current = toggleViewBtn.getAttribute('data-view') || 'list';
  // toggle between 'list' and 'grid' (grid = more compact side-by-side cards)
  const next = current === 'list' ? 'grid' : 'list';
  toggleViewBtn.setAttribute('data-view', next);
  toggleViewBtn.setAttribute('aria-pressed', next === 'grid' ? 'true' : 'false');

  if(next === 'grid'){
    catalogEl.classList.add('grid-view');
    catalogEl.style.display = 'grid';
    catalogEl.style.gridTemplateColumns = 'repeat(auto-fit, minmax(20rem, 1fr))';
    catalogEl.style.gap = '1.6rem';
    Array.from(catalogEl.children).forEach(row => {
      row.style.display = 'block';
      row.style.gridTemplateColumns = '';
      row.querySelector('.course-image').style.aspectRatio = '4/3';
      row.querySelector('.course-content').style.padding = '.8rem 0';
    });
    gridIcon.style.display = 'none';
    listIcon.style.display = '';
  } else {
    catalogEl.classList.remove('grid-view');
    catalogEl.style.display = '';
    catalogEl.style.gridTemplateColumns = '';
    catalogEl.style.gap = '';
    Array.from(catalogEl.children).forEach(row => {
      row.style.display = '';
      row.querySelector('.course-image').style.aspectRatio = '16/9';
    });
    gridIcon.style.display = '';
    listIcon.style.display = 'none';
  }
});

// init
document.addEventListener('DOMContentLoaded', () => {
  buildCategoryOptions();
  renderCatalog();
  // ensure default icons state
  gridIcon.style.display = '';
  listIcon.style.display = 'none';
  // add default option "all" explicitly
  const allOpt = document.createElement('option');
  allOpt.value = 'all';
  allOpt.textContent = 'Todos';
  // if 'all' not present, ensure select begins with "Categoria" as first option (already in HTML)
});
