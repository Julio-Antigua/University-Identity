const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: true
})


const fs = require('fs');

const path = require('path');

module.exports = {

  devServer: {

    host: 'localhost',

    port: 8080,

    https: {

      key: fs.readFileSync(path.resolve(__dirname, 'localhost-key.pem')),

      cert: fs.readFileSync(path.resolve(__dirname, 'localhost.pem'))

    }

  }

}