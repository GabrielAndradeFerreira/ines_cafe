const products = [
  {
    id: 1,
    title: "Kit Degustação INESCAFÉ - 4 Cafés (4x250g)",
    short: "Inclui: Intenso, Suave, Gourmet e um exclusivo Café Especial da Casa.",
    price: 109.90,
    category: "Kits",
    image:  "./img/kit4cafe.png",
  },
  {
    id: 2,
    title: "Blend Especial - 250g",
    short: "Blend selecionado, torra média, notas de caramelo.",
    price: 34.90,
    category: "Cafés",
    image: "./img/especial2.png",
  },
  {
    id: 3,
    title: "Cápsulas Premium - 10 un.",
    short: "Compatível com máquinas Nespresso, torra escura.",
    price: 24.50,
    category: "Cápsulas",
    image: "./img/capsulas.png",
  },
  {
    id: 4,
    title: "Kit Degustação INESCAFÉ - 3 Cafés (4x250g)",
    short: "Inclui: Intenso, Suave e um exclusivo Café Especial da Casa.",
    price: 109.90,
    category: "Kits",
    image:  "./img/kit3cafe.png",
  },
  {
    id: 5,
    title: "Moedor Manual",
    short: "Compacto e resistente, perfeito para viagens.",
    price: 79.00,
    category: "Acessórios",
    image:  "./img/moedor.png",
  },
  {
    id: 6,
    title: "Blend Especial - 250g (Descafeinado)",
    short: "Descafeinado com processo natural, notas frutadas.",
    price: 39.90,
    category: "Cafés",
    image: "./img/especial.png",
  }
];

let state = {
  products: [...products],
  sortDir: 'desc', // 'desc' or 'asc'
  category: 'all',
  view: 'grid' // 'grid' or 'list'
};

function formatPrice(v){
  return v.toLocaleString('pt-BR', { style:'currency', currency:'BRL' });
}

function buildCategoryOptions(){
  const select = document.getElementById('categorySelect');
  const cats = Array.from(new Set(products.map(p=>p.category)));
  cats.forEach(cat=>{
    const opt = document.createElement('option');
    opt.value = cat;
    opt.textContent = cat;
    select.appendChild(opt);
  });
}

function placeholderDataURI(text = 'Imagem', color = '#d9bdbf'){
  const svg = `<svg xmlns='http://www.w3.org/2000/svg' width='800' height='480'>
    <rect width='100%' height='100%' fill='${color}'/>
    <g>
      <text x='50%' y='50%' dominant-baseline='middle' text-anchor='middle' font-family='Montserrat, Arial' font-size='36' fill='#7a4b50'>${text}</text>
    </g>
  </svg>`;
  return 'data:image/svg+xml;utf8,' + encodeURIComponent(svg);
}

// render
function render(){
  const container = document.getElementById('catalog');
  container.innerHTML = '';

  // filter
  let list = products.filter(p => state.category === 'all' ? true : p.category === state.category);

  // sort
  list.sort((a,b)=>{
    if(state.sortDir === 'desc') return b.price - a.price;
    return a.price - b.price;
  });

  // update container class for view
  container.classList.toggle('grid-view', state.view === 'grid');
  container.classList.toggle('list-view', state.view === 'list');

  // create cards
  list.forEach(p=>{
    const card = document.createElement('article');
    card.className = 'card';
    card.setAttribute('data-id', p.id);

    // image wrap
    const imgWrap = document.createElement('div');
    imgWrap.className = 'img-wrap';

    const img = document.createElement('img');
    img.className = 'img';
    img.alt = p.title;

    imgWrap.appendChild(img);

    const content = document.createElement('div');
    content.className = 'card-content';

    const title = document.createElement('h3');
    title.innerHTML = `☕ ${p.title}`;

    const subtitle = document.createElement('div');
    subtitle.className = 'subtitle';
    subtitle.textContent = p.category;

    const desc = document.createElement('p');
    desc.textContent = p.short;

    const more = document.createElement('a');
    more.className = 'more';
    more.href = '#';
    more.textContent = 'Saiba Mais+';
    more.addEventListener('click', (e)=>{
      e.preventDefault();
      alert(`${p.title}\n\n${p.short}\n\nPreço: ${formatPrice(p.price)}`);
    });

    const price = document.createElement('div');
    price.className = 'price';
    price.textContent = `Preço: ${formatPrice(p.price)}`;

    // assemble
    content.appendChild(title);
    content.appendChild(subtitle);
    content.appendChild(desc);
    content.appendChild(more);
    content.appendChild(price);

    card.appendChild(imgWrap);
    card.appendChild(content);

    container.appendChild(card);
  });
}

function setupControls(){
  const sortBtn = document.getElementById('sortBtn');
  sortBtn.addEventListener('click', ()=>{
    // direction
    state.sortDir = state.sortDir === 'desc' ? 'asc' : 'desc';
    sortBtn.setAttribute('data-direction', state.sortDir);
    sortBtn.setAttribute('aria-pressed', state.sortDir === 'asc' ? 'true' : 'false');
    sortBtn.firstChild && sortBtn.firstChild.nodeType; // noop to satisfy linter

    if(state.sortDir === 'desc') sortBtn.innerHTML = 'Preço Maior ao menor <span class="icon sort-icon" aria-hidden="true">' + sortBtnIconSVG() + '</span>';
    else sortBtn.innerHTML = 'Preço Menor ao maior <span class="icon sort-icon" aria-hidden="true">' + sortBtnIconSVG() + '</span>';
    render();
  });

  const select = document.getElementById('categorySelect');
  select.addEventListener('change', (e)=>{
    state.category = e.target.value;
    render();
  });

  const toggle = document.getElementById('toggleView');
  toggle.addEventListener('click', ()=>{
    state.view = state.view === 'grid' ? 'list' : 'grid';
    toggle.setAttribute('aria-pressed', state.view === 'list' ? 'true' : 'false');

    document.getElementById('gridIcon').style.display = state.view === 'grid' ? '' : 'none';
    document.getElementById('listIcon').style.display = state.view === 'list' ? '' : 'none';
    render();
  });

  const sortBtnInit = document.getElementById('sortBtn');
  sortBtnInit.innerHTML = 'Preço Maior ao menor <span class="icon sort-icon" aria-hidden="true">' + sortBtnIconSVG() + '</span>';
}

function sortBtnIconSVG(){
  return `<svg viewBox="0 0 24 24" width="1.2rem" height="1.2rem" fill="none" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"><path d="M3 6h13M3 12h9M3 18h5"/></svg>`;
}

document.addEventListener('DOMContentLoaded', ()=>{
  buildCategoryOptions();
  setupControls();
  render();
});




// render
function render(){
  const container = document.getElementById('catalog');
  container.innerHTML = '';

// filter
  let list = products.filter(p => state.category === 'all' ? true : p.category === state.category);

  // sort
  list.sort((a,b)=>{
    if(state.sortDir === 'desc') return b.price - a.price;
    return a.price - b.price;
  });

  // update container class for view
  container.classList.toggle('grid-view', state.view === 'grid');
  container.classList.toggle('list-view', state.view === 'list');

  // create cards
  list.forEach(p=>{
    const card = document.createElement('article');
    card.className = 'card';
    card.setAttribute('data-id', p.id);

    // image wrap
    const imgWrap = document.createElement('div');
    imgWrap.className = 'img-wrap';

    const img = document.createElement('img');
    img.className = 'img';
    img.alt = p.title;

    // <--- ADIÇÃO ESSENCIAL: Atribui o caminho da imagem ou o placeholder
    if (p.image) {
        img.src = p.image;
    } else {
        // Usa o placeholder se 'image' for null
        img.src = placeholderDataURI(p.title); 
    }
    // FIM DA ADIÇÃO

    imgWrap.appendChild(img);

    const content = document.createElement('div');
    content.className = 'card-content';

    const title = document.createElement('h3');
    title.innerHTML = `☕ ${p.title}`;

    const subtitle = document.createElement('div');
    subtitle.className = 'subtitle';
    subtitle.textContent = p.category;

    const desc = document.createElement('p');
    desc.textContent = p.short;

    const more = document.createElement('a');
    more.className = 'more';
    more.href = '#';
    more.textContent = 'Saiba Mais+';
    more.addEventListener('click', (e)=>{
      e.preventDefault();
      alert(`${p.title}\n\n${p.short}\n\nPreço: ${formatPrice(p.price)}`);
    });

    const price = document.createElement('div');
    price.className = 'price';
    price.textContent = `Preço: ${formatPrice(p.price)}`;

    // assemble
    content.appendChild(title);
    content.appendChild(subtitle);
    content.appendChild(desc);
    content.appendChild(more);
    content.appendChild(price);

    card.appendChild(imgWrap);
    card.appendChild(content);

    container.appendChild(card);
  });
}
