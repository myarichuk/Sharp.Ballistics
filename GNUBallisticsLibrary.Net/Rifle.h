#pragma once

using namespace System;
using namespace Sharp::Ballistics::Abstractions;
using namespace UnitsNet;
using namespace System::Collections::Generic;

#define ZERO_RANGE 100
public ref class Rifle : IRifle 
{
private:
	RifleInfo^ rifleInfo;
	ScopeInfo^ scopeInfo;
	AmmoInfo^ ammoInfo;

	double zeroAngle;

	String^ id;
public:
	Rifle() 
	{
	}

	Rifle(RifleInfo^ rifleInfo, ScopeInfo^ scopeInfo, AmmoInfo^ ammoInfo)
	{
		Initialize(rifleInfo, scopeInfo, ammoInfo);
	}

	void Initialize(RifleInfo^ rifleInfo, ScopeInfo^ scopeInfo, AmmoInfo^ ammoInfo)
	{
		if (rifleInfo == nullptr)
			throw gcnew System::ArgumentNullException("rifleInfo");

		if (rifleInfo->ZeroingConditions == nullptr)
			throw gcnew System::ArgumentNullException("rifleInfo.ZeroingConditions");

		if (scopeInfo == nullptr)
			throw gcnew System::ArgumentNullException("scopeInfo");
		if (ammoInfo == nullptr)
			throw gcnew System::ArgumentNullException("ammoInfo");

		this->rifleInfo = rifleInfo;
		this->scopeInfo = scopeInfo;
		this->ammoInfo = ammoInfo;

		zeroAngle = ZeroAngle((int)ammoInfo->DragFunction,
			AtmCorrect(ammoInfo->BC,
				rifleInfo->ZeroingConditions->Altitude.Feet,
				rifleInfo->ZeroingConditions->Barometer.Psi * 2.03602, //convert to Hg
				rifleInfo->ZeroingConditions->Temperature.DegreesFahrenheit,
				rifleInfo->ZeroingConditions->RelativeHumidity),
			ammoInfo->MuzzleVelocity.FeetPerSecond,
			scopeInfo->Height.Inches,
			scopeInfo->ZeroDistance.Yards, 0);
	}

	virtual ShotInfo^ Solve(
		double shootingAngle,
		Speed windSpeed,
		double windAngle,
		Length range,
		AtmosphericInfo^ atmInfo);

	virtual IEnumerable<ShotInfo^>^ SolveMultiple(double shootingAngle,
		Speed windSpeed,
		double windAngle,
		IEnumerable<Length>^ ranges,
		AtmosphericInfo^ atmInfo);

	virtual property String^ Name
	{
		String^ get()
		{
			if (rifleInfo == nullptr)
				return nullptr;
			return rifleInfo->Name;
		}
		void set(String^ val)
		{
			rifleInfo->Name = val;
		}
	}

	virtual property AtmosphericInfo^ ZeroingConditions
	{
		AtmosphericInfo^ get()
		{
			if (rifleInfo == nullptr)
				return nullptr;
			return rifleInfo->ZeroingConditions;
		}
		void set(AtmosphericInfo^ val)
		{
			rifleInfo->ZeroingConditions = val;
		}
	}

	virtual property AmmoInfo^ Ammo
	{
		AmmoInfo^ get()
		{
			return ammoInfo;
		}

		void set(AmmoInfo^ val)
		{
			ammoInfo = val;
		}
	}

	virtual property ScopeInfo^ Scope
	{
		ScopeInfo^ get()
		{
			return scopeInfo;
		}
		void set(ScopeInfo^ val)
		{
			scopeInfo = val;
		}
	}

	public:
		virtual property String^ Id
		{
			String^ get()
			{
				return id;
			}
		private:
			void set(String^ val)
			{
				id = val;
			}
		}
};
