using System;
using System.Collections.Generic;
using System.Text;

namespace de.christianleberfinger.dotnet.IO.commandhandling {

	public interface ICommand<ID> {

		ID CommandIdentifier
		{
			get;
		}
	
	}

}
