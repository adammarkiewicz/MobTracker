const { createProxyMiddleware } = require("http-proxy-middleware");

module.exports = function(app) {
  app.use(
    createProxyMiddleware("/api/v1", {
      target: "https://127.0.0.1:44375",
      ws: true,
      secure: false
    })
  );

  app.use(
    createProxyMiddleware("/api/test", {
      target: "https://127.0.0.1:44375",
      secure: false
    })
  );
};
