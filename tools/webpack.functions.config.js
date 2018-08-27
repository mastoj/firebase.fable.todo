const path = require("path");
const fableUtils = require("fable-utils");
const nodeExternals = require('webpack-node-externals');
const nodemonPlugin = require('nodemon-webpack-plugin');

function resolve(filePath) {
    return path.join(__dirname, filePath)
}

var babelOptions = fableUtils.resolveBabelOptions({
    presets: [
        [
            "env",
            {
                "targets": {
                    "node": "6"
                },
                "modules": "commonjs"
            }
        ]
    ]
});

var isProduction = process.argv.indexOf("-p") >= 0;
console.log("Bundling for " + (isProduction ? "production" : "development") + "...");

var basicConfig = {
    devtool: "source-map",
    resolve: {
        modules: [
            resolve("../src/Todo.Functions/node_modules/")
        ]
    },
    mode: isProduction ? "production" : "development",
    module: {
        rules: [{
            test: /\.fs(x|proj)?$/,
            use: {
                loader: "fable-loader",
                options: {
                    babel: babelOptions,
                    define: isProduction ? [] : ["DEBUG"],
                    extra: {
                        optimizeWatch: true
                    }
                },
            }
        }]
    }
};

let serverConfig = Object.assign({
    context: resolve('../src/Todo.Functions'),
    target: "node",
    node: {
        __filename: false,
        __dirname: false
    },
    externals: [nodeExternals()],
    entry: resolve("../src/Todo.Functions/Todo.Functions.fsproj"),
    output: {
        path: resolve("../functions/"),
        filename: "index.js",
        libraryTarget: "commonjs2"
    },
    plugins: [
        new nodemonPlugin()
    ],
    optimization: {
        minimize: false
    }
}, basicConfig);

module.exports = serverConfig