/** @type {import('tailwindcss').Config} */

const defaultTheme = require('tailwindcss/defaultTheme')

module.exports = {
  content: [],
  theme: {
    fontFamily: {
      'sans': ['Inter', ...defaultTheme.fontFamily.sans],
      'mono': ['Fira Code', ...defaultTheme.fontFamily.mono]
    }
  },
  plugins: [],
}

