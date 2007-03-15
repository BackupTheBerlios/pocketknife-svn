using System;
using System.Collections.Generic;
using System.Text;

namespace de.christianleberfinger.dotnet.IO.commandhandling {

	public class CommandManager<ID, HANDLER, CMD> where HANDLER : ICommandHandler<ID,CMD> where CMD:ICommand<ID>
	{
		Dictionary<ID, HANDLER> commandHandlers = new Dictionary<ID,HANDLER>();

		public void addHandler(HANDLER commandHandler)
		{
			foreach(ID commandID in commandHandler.CommandIdentifiers)
				commandHandlers.Add(commandID, commandHandler);
		}

		public void handleCommand(CMD command)
		{
			HANDLER commandHandler;

			bool handlerFound = commandHandlers.TryGetValue(command.CommandIdentifier, out commandHandler);
			if (handlerFound)
				commandHandler.handleCommand(command);
		}
	}

}
