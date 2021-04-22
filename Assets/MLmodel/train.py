import torch
import torch.autograd as autograd
import torch.nn as nn
import torch.functional as F
import torch.optim as optim

from torch.nn.utils.rnn import pack_padded_sequence, pad_packed_sequence

class MotionPredict(nn.Module):

	def __init__(self, vocab_size, hidden_dim, output_size):

		super(MotionPredict, self).__init__()

		self.hidden_dim = hidden_dim
		self.vocab_size = vocab_size

		self.lstm = nn.GRU(vocab_size, hidden_dim, num_layers=1)

		# self.hidden2out = nn.Linear(hidden_dim, output_size)
		
		self.dropout_layer = nn.Dropout(p=0.2)


	def init_hidden(self, batch_size):
		return(autograd.Variable(torch.randn(1, batch_size, self.hidden_dim)))

	def forward(self, batch,hidden):
		# self.hidden = self.init_hidden(batch.size(1))
		# embeds = self.embedding(batch)
		# packed_input = pack_padded_sequence(embeds, lengths)
		outputs, (ht,c0) = self.lstm(batch, hidden)
		# ht is the last hidden state of the sequences
		# ht = (1 x batch_size x hidden_dim)
		# ht[-1] = (batch_size x hidden_dim)
		# output = self.dropout_layer(ht[-1])
		# output = self.hidden2out(output)
		
		return outputs
# model = nn.LSTM(10,10,num_layers =  1)
# model.eval()
# with torch.no_grad():
#     dummy_input = torch.randn([20,1,10]) 
#     print(model(dummy_input))
#     torch.onnx.export(model, dummy_input, "motion_predict.onnx", verbose=False,
#                         input_names=['input'],
#                         output_names=['output','hn'],
#                         dynamic_axes={'input': {0: 'sequence'}, 'output': {0: 'sequence'}})
model = nn.LSTM(10,3)
model.eval()
with torch.no_grad():
    dummy_input = torch.randn([5,1,10])# ,(torch.randn([1,1,3]),torch.randn([1,1,3])))
    # print(model(dummy_input,(torch.randn([1,1,3]),torch.randn([1,1,3]))))
    torch.onnx.export(model, dummy_input, "motion_predict.onnx", verbose=False,
                        input_names=['input',"h0","c0"],
                        output_names=['output',"hn","cn"],
                        dynamic_axes={'input': {0: 'sequence'}, 'output': {0: 'sequence'}})

