var numeros = [2, 3, 5, 8, "lobo"];

/*const doubleNumbers = numeros.map((num) => {
  if (typeof num !== "number") {
    console.log("Erro");
  } else {
    console.log("Todos numeros");
  }

  return num * 2;
});*/

try {
  const doubleNumbers = numeros.map((num) => {
    if (typeof num !== "number") {
      console.log("Erro");
    } else {
      console.log("Todos numeros");
    }

    return num * 2;
  });
} catch (erro) {
  console.log(erro);
}
