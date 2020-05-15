
import threading
import time,random

def tempo():
    t = random.randint(3,7)    
    time.sleep(t)

class Cliente:
    def __init__(self, idx, name, sobrenome, cpf, telefone):
        self.name=None
        self.sobrenome=None
        self.cpf=None
        self.telefone=None 
        self.idx = idx  
        tempo()
        
def novo_cliente(clis, idx):    
    print(f"{idx}-criando...")
    clis.new(name="relampago", sobrenome="marquinhos", cpf=f"123.456.789-{idx}", telefone="4002-8922")
    print(f"{idx}-criado")


class Clientes_s:
    def __init__(self):
        self.semaphore = threading.Semaphore()
        self._clientes = []
        
    def new(self, *args, **kwargs):
        
        self.semaphore.acquire()
        cliente = Cliente(len(self._clientes), *args, **kwargs)        
        self._clientes.append(cliente)
        self.semaphore.release()
        return cliente

class Clientes_l:
    def __init__(self):
        self.lock = threading.Lock() 
        self._clientes = []
        
    def new(self, *args, **kwargs):
        
        with self.lock:
            cliente = Cliente(len(self._clientes), *args, **kwargs)            
            self._clientes.append(cliente)  
            
        return cliente

if __name__ == "__main__":
    clis1 = Clientes_l()
    thrs1 = []
    
    for pos in range(2):
        thr = threading.Thread(target=novo_cliente, args=(clis,pos,))
        thr.start()
        thrs1.append(thr)

    for thr in thrs1:
        thr.join()

    print(clis1._clientes[0].idx)
    print(clis1._clientes[1].idx)


    clis2 = Clientes_s()
    thrs2 = []
    
    for pos in range(2):
        thr = threading.Thread(target=novo_cliente, args=(clis,pos,))
        thr.start()
        thrs2.append(thr)

    for thr in thrs2:
        thr.join()
    
    print(clis2._clientes[0].idx)
    print(clis2._clientes[1].idx)