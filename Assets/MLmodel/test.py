import onnx
onnx_model = onnx.load("motion_predict.onnx")
print(onnx_model)
