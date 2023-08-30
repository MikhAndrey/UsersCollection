using UsersCollectionAPI.Model.Infrastructure.Interfaces;
using UsersCollectionAPI.Model.Repositories;
using UsersCollectionAPI.Model.Repositories.Interfaces;

namespace UsersCollectionAPI.Model.Infrastructure;

public class UnitOfWork : IDisposable, IUnitOfWork
{
	private readonly ApplicationDbContext _context;

	private IUserRepository? _userRepository;

	private bool _disposed;

	public UnitOfWork(ApplicationDbContext context)
	{
		_context = context;
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	public IUserRepository Users
	{
		get
		{
			_userRepository ??= new UserRepository(_context);
			return _userRepository;
		}
	}

	public async Task SaveAsync()
	{
		await _context.SaveChangesAsync();
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!_disposed)
		{
			if (disposing)
			{
				_context.Dispose();
			}

			_disposed = true;
		}
	}
}

