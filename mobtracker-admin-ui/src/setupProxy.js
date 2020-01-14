const proxy = require("http-proxy-middleware");

module.exports = function(app) {
  app.use(
    "/api",
    proxy({
      target: "https://localhost:44375",
      ws: true,
      secure: false
    })
  );

  app.use(
    "/test",
    proxy({
      target: "https://localhost:44375",
      secure: false
    })
  );
};
