# import UnityEngine
# from TMPro import TextMeshProUGUI

# from langchain_huggingface import HuggingFaceEndpoint
# from langchain_core.messages import (
#     HumanMessage,
#     SystemMessage,
#     AIMessage
# )
# from langchain_core.prompts import ChatPromptTemplate, MessagesPlaceholder
# from langchain_community.chat_message_histories.in_memory import ChatMessageHistory
# from langchain_core.chat_history import BaseChatMessageHistory
# from langchain_core.runnables.history import RunnableWithMessageHistory

# # Hugging Face model
# model = HuggingFaceEndpoint(
#   endpoint_url="https://fmgwp6ay6iqnx2x1.us-east-1.aws.endpoints.huggingface.cloud",
#   max_new_tokens=50,
#   top_k=10,
#   top_p=0.95,
#   typical_p=0.95,
#   temperature=0.7,
#   repetition_penalty=1.03)

# # Create chat template
# template = ChatPromptTemplate([
#     ("system", "Your name is {fairy_name} You are a fairy-like girl who lives in a forest in the clouds. You act as a virtual friend to those who play the VR game in which you are hosted."),
#     HumanMessage(
#         content="Hello!"
#     ),
#     AIMessage(
#         content="Welcome to the fairy forest!"
#     ),
#     MessagesPlaceholder(variable_name="history"),
#     HumanMessage(content="{user_input}")
# ])

# # Define runnable
# runnable = template | model

# # Initalize storage
# store = {}

# # Function to update history
# def get_session_history(session_id: str) -> BaseChatMessageHistory:
#     if session_id not in store:
#         store[session_id] = ChatMessageHistory()
#     return store[session_id]

# # Define runnable with message history
# with_message_history = RunnableWithMessageHistory(
#     runnable,
#     get_session_history,
#     input_messages_key="user_input",
#     history_messages_key="history",
# )

# # Get the object that holds the transcribed text
# jackie_object = UnityEngine.GameObject.Find("text-transcription")
# jackie_text_component = jackie_object.GetComponent[TextMeshProUGUI]()

# # Get the object that will hold the response text
# jolleen_object = UnityEngine.GameObject.Find("text-response")
# jolleen_text_component = jolleen_object.GetComponent[TextMeshProUGUI]()

# # Get response from model with transcribed text as input
# output = model.invoke(jackie_text_component.text)

# # Set response text to output
# jackie_text_component.text = output

# # Print output to console
# UnityEngine.Debug.Log(f'Output: {output}')