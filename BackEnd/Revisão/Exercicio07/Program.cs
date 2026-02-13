using Exercicio07;

Veiculo veiculo1 = new Veiculo();
veiculo1.Mover();  
Carro carro = new Carro();
carro.Mover(); 

Bicicleta bike = new Bicicleta();
bike.Mover();  

// Polimorfismo
Veiculo veiculo2 = new Carro();
veiculo2.Mover();   
