/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    extend: {
      colors: {
        'default': '#e9e9e9',
        // https://paletadecores.com/paleta/cdf7f2/bbe4df/a9d2cd/96bfba/84aca7/
        'custom_0': '#cdf7f2',
        'custom_1': '#bbe4df',
        'custom_2': '#a9d2cd', 
        'custom_3': '#96bfba',
        'custom_4': '#84aca7'
      },
      height: {
        '512': '32rem'
      }
    },
  },
  plugins: [],
}
