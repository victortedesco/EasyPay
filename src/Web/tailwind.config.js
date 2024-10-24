/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    extend: {
      colors: {
        'bg-color': '#e9e9e9',
        // https://paletadecores.com/paleta/cdf7f2/bbe4df/a9d2cd/96bfba/84aca7/
        'custom-0': '#cdf7f2',
        'custom-1': '#bbe4df',
        'custom-2': '#a9d2cd', 
        'custom-3': '#96bfba',
        'custom-4': '#84aca7'
      }
    },
  },
  plugins: [],
}
