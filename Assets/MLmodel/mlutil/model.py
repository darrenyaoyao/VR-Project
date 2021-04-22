import torch
import torch.autograd as autograd
import torch.nn as nn
import torch.functional as F
import torch.optim as optim

class MotionPredict(nn.Module):

    def __init__(self, input_size, hidden_dim,input_length = 50,output_length = 15):
        super(MotionPredict, self).__init__()
        self.input_size = input_size
        self.input_length = input_length
        self.output_length = output_length
        self.lstmcell = nn.LSTMCell(input_size, hidden_dim)
        self.linear = nn.Linear(hidden_dim, input_size)
        self.dropout = nn.Dropout(p=0.2)

    def forward(self, x):
        for i in range(self.input_length):
            if i==0:
                h,c = self.lstmcell(x[i,:,:])        
            else:
                h,c = self.lstmcell(x[i,:,:],(h,c))
        y = torch.Tensor(self.output_length, x.size(1), self.input_size)
        for i in range(self.output_length):
            output = self.linear(h)
            y[i,:,:]=output
            h,c = self.lstmcell(output,(h,c))
        return y

if __name__=="__main__":
    model = MotionPredict( input_size = 9, hidden_dim = 20)
    a = torch.randn(50,1,9)
    b = model(a)
    pass