const webConfig = require("./tools/webpack.web.config")
const functionsConfig = require("./tools/webpack.functions.config")

module.exports = [webConfig, functionsConfig];
//module.exports = [functionsConfig];