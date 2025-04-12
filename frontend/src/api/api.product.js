// Giả lập API
const fakeProducts = [
    { id: 1, name: 'Bánh mặn đặc biệt', type: 'banh-man', price: 25000, discount: 5, description: 'Bánh truyền thống...' },
    { id: 2, name: 'Trà sữa trân châu', type: 'do-uong', price: 35000, discount: 10, description: 'Trà sữa thơm ngon...' }
  ];
  
  export const fetchProducts = () => 
    new Promise(resolve => setTimeout(() => resolve([...fakeProducts]), 500));
  
  export const createProduct = (product) =>
    new Promise(resolve => setTimeout(() => {
      const newProduct = { ...product, id: Date.now() };
      fakeProducts.push(newProduct);
      resolve(newProduct);
    }, 500));
  
  export const updateProduct = (product) =>
    new Promise(resolve => setTimeout(() => {
      const index = fakeProducts.findIndex(p => p.id === product.id);
      fakeProducts.splice(index, 1, product);
      resolve(product);
    }, 500));
  
  export const deleteProductAPI = (id) =>
    new Promise(resolve => setTimeout(() => {
      const index = fakeProducts.findIndex(p => p.id === id);
      fakeProducts.splice(index, 1);
      resolve();
    }, 500));