using System;
using System.Collections.Generic;
using System.Text;

namespace de.christianleberfinger.dotnet.IO.commandhandling {

	public interface ICommandHandler<ID, CMD> {

		ID[] CommandIdentifiers { get; }

		void handleCommand(CMD command);

	}

}
