const webpack = require('webpack');  
var path = require('path');  
const MiniCssExtractPlugin = require('mini-css-extract-plugin');

module.exports = [  
    {
        stats: {
            entrypoints: false,
            children: false
        },
        entry: {
            'bundle': './ClientApp/app.js',
        },

        output: {
            path: path.resolve('./wwwroot'),
            filename: '[name].js'
        },

        resolve: {
            mainFields: ['browser', 'main', 'module'],
            extensions: ['.js', '.json']
        },

        module: {
            rules: [
                { test: /\.js/, use: [{
                    loader: 'babel-loader'
                }], exclude: /node_modules/ },
                {
                    test: /\.css$/, use: [
                        MiniCssExtractPlugin.loader,
                        'css-loader',
                      ],
                },
                { test: /\.flow/, use: [{
                    loader: 'ignore-loader'
                }] }
            ]
        },

        plugins: [
            new MiniCssExtractPlugin({filename: 'style.css'})
        ]
    }
];