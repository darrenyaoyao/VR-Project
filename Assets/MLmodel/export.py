import torch
from mlutil.model import MotionPredict

model = MotionPredict(9,20,50,25)
model.eval()
with torch.no_grad():
    dummy_input = torch.randn(50,1,9)
    torch.onnx.export(model, dummy_input, "motion_predict.onnx", verbose=False,
                        input_names=['input'],
                        output_names=['output'],
                        opset_version=11,
                        export_params=True, 
                        do_constant_folding=True,
                        # dynamic_axes={'input': {0: 'sequence'}, 'output': {0: 'sequence'}}
                        )

